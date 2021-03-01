using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace CodeGenerator.Infrastructure
{
    public interface IRedis
    {
        ITransaction GetTransaction(bool isRead = false);
        IDatabase GetDatabase(bool isRead = false);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<bool> KeyDeleteAsync(string key, CommandFlags flag = CommandFlags.None);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<bool> KeyExistsAsync(string key, CommandFlags flag = CommandFlags.None);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(string key, DateTime? expiry, CommandFlags flag = CommandFlags.None);
        Task<bool> KeyExpireAsync(string key, TimeSpan? expiry, CommandFlags flag = CommandFlags.None);
        Task<RedisValue> GetStringAsync(string key, CommandFlags flag = CommandFlags.None);
        Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = default, When when = When.Always, CommandFlags flags = CommandFlags.None);
        Task<bool> Lock(string key, int managedThreadId, TimeSpan expiry);
        Task<bool> UnLock(string key, int managedThreadId);

    }
}
