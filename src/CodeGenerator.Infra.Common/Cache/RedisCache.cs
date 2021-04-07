using System;
using System.Threading;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace CodeGenerator.Infra.Common.Cache
{
    public class RedisCache : ICache
    {
        private static RedisOptions _redisOptions;

        public RedisCache(IOptions<RedisOptions> options)
        {
            _redisOptions = options.Value;
        }

        private readonly Lazy<ConnectionMultiplexer> _writeConnection = new Lazy<ConnectionMultiplexer>(
            () => ConnectionMultiplexer.Connect(_redisOptions.MasterServer)
        );
        private readonly Lazy<ConnectionMultiplexer> _readConnection = new Lazy<ConnectionMultiplexer>(
            () =>
            {
                //随机选择
                var max = _redisOptions.SlaveServer.Count;
                var slaveIndex = new Random().Next(0, max);
                return ConnectionMultiplexer.Connect(_redisOptions.SlaveServer[slaveIndex]);
            }
        );

        private ConnectionMultiplexer WriteConnection => _writeConnection.Value;
        private ConnectionMultiplexer ReadConnection => _readConnection.Value;

        private ITransaction GetTransaction(bool isRead = false)
        {
            return this.GetDatabase().CreateTransaction();
        }

        private IDatabase GetDatabase(bool isRead = false)
        {
            return isRead
                ? ReadConnection.GetDatabase(_redisOptions.DefaultDatabase)
                : WriteConnection.GetDatabase(_redisOptions.DefaultDatabase);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string key, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).KeyDeleteAsync(key, flag);
        /// <summary>
        /// Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).KeyExistsAsync(key, flag);
        /// <summary>
        /// 设置Key超时时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry">时间</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public async Task<bool> SetKeyExpireAsync(string key, DateTime? expiry, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).KeyExpireAsync(key, expiry, flag);
        /// <summary>
        /// 设置Key超时时间间隔
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timespan">时间间隔</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public async Task<bool> SetKeyExpireAsync(string key, TimeSpan? timespan, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).KeyExpireAsync(key, timespan, flag);
        /// <summary>
        /// 获取字符串数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public async Task<RedisValue> GetStringAsync(string key, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).StringGetAsync(key, flag);
        /// <summary>
        /// 设置字符串数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = default, When when = When.Always,
            CommandFlags flags = CommandFlags.None)
            => await GetDatabase().StringSetAsync(key, value, expiry, when, flags);

        /// <summary>
        /// 加锁,建议在 finally 中 unlock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">Thread.CurrentThread.ManagedThreadId</param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task Lock(string key, int value, TimeSpan expiry = default)
        {
            int sleepMilliseconds = 300;
            var times = expiry.Milliseconds / sleepMilliseconds;
            if (expiry.Minutes > 10) throw new Exception("锁定时间不能大于10分钟！");
            while (true)
            {
                var lockFlag = await GetDatabase().LockTakeAsync(key, value, expiry);
                if (lockFlag)
                {
                    break;
                }
                else
                {
                    times--;
                    Thread.Sleep(sleepMilliseconds);
                }

                if (times == 0)
                {
                    throw new Exception("无法获取数据锁，请稍后再试！");
                }
            }

        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">Thread.CurrentThread.ManagedThreadId</param>
        /// <returns></returns>
        public async Task UnLock(string key, int value)
        {
            while (true)
            {
                var unLockFlag = await GetDatabase().LockReleaseAsync(key, value);
                if (unLockFlag)
                {
                    return;
                }
                Thread.Sleep(1000);
            }

        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            WriteConnection?.Dispose();
            ReadConnection?.Dispose();
        }
    }
}
