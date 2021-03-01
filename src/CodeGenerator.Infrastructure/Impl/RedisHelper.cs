using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<bool> KeyDeleteAsync(string key, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).KeyDeleteAsync(key, flag);

        public async Task<bool> KeyExistsAsync(string key, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).KeyExistsAsync(key, flag);

        public async Task<bool> KeyExpireAsync(string key, DateTime? expiry, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).KeyExpireAsync(key, expiry, flag);

        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).KeyExpireAsync(key, expiry, flag);

        public async Task<RedisValue> GetStringAsync(string key, CommandFlags flag = CommandFlags.None)
            => await GetDatabase(true).StringGetAsync(key, flag);

        public async Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = default, When when = When.Always,
            CommandFlags flags = CommandFlags.None)
            => await GetDatabase().StringSetAsync(key, value, expiry, when, flags);

        /// <summary>
        /// 加锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="managedThreadId">Thread.CurrentThread.ManagedThreadId</param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> Lock(string key, int managedThreadId, TimeSpan expiry)
        {
            try
            {
                var times = 3;
                while (true)
                {
                    var lockFlag = await GetDatabase().LockTakeAsync(key, managedThreadId, expiry);
                    if (lockFlag)
                    {
                        return true;
                    }
                    else
                    {
                        times--;
                        Thread.Sleep(expiry);
                    }

                    if (times == 0)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="managedThreadId">Thread.CurrentThread.ManagedThreadId</param>
        /// <returns></returns>
        public async Task<bool> UnLock(string key, int managedThreadId)
        {
            try
            {
                var unLockFlag = await GetDatabase().LockReleaseAsync(key, managedThreadId);
                return unLockFlag;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
