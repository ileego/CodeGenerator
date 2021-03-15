using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeGenerator.Infrastructure.Options;
using IRedis = CodeGenerator.Infrastructure.IRedis;

namespace CodeGenerator.Infrastructure.Impl
{
    public class RedisHelper : IRedis
    {
        private static RedisOptions _redisOptions;

        public RedisHelper(IOptions<RedisOptions> options)
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

        public ConnectionMultiplexer WriteConnection => _writeConnection.Value;
        public ConnectionMultiplexer ReadConnection => _readConnection.Value;

        public ITransaction GetTransaction(bool isRead = false)
        {
            return this.GetDatabase().CreateTransaction();
        }

        public IDatabase GetDatabase(bool isRead = false)
        {
            return isRead
                ? ReadConnection.GetDatabase(_redisOptions.CurrentDb)
                : WriteConnection.GetDatabase(_redisOptions.CurrentDb);
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
        /// 加锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">Thread.CurrentThread.ManagedThreadId</param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task Lock(string key, int value, TimeSpan expiry)
        {
            var times = expiry.Ticks / 500;
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
                    Thread.Sleep(expiry);
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
                    break;
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
