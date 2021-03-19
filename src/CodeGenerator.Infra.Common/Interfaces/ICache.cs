using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace CodeGenerator.Infra.Common.Interfaces
{
    public interface ICache : IDisposable
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<bool> KeyDeleteAsync(string key, CommandFlags flag = CommandFlags.None);
        /// <summary>
        /// Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<bool> KeyExistsAsync(string key, CommandFlags flag = CommandFlags.None);
        /// <summary>
        /// 设置Key超时时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry">时间</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<bool> SetKeyExpireAsync(string key, DateTime? expiry, CommandFlags flag = CommandFlags.None);
        /// <summary>
        /// 设置Key超时时间间隔
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timespan">间隔</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<bool> SetKeyExpireAsync(string key, TimeSpan? timespan, CommandFlags flag = CommandFlags.None);
        /// <summary>
        /// 获取字符串数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        Task<RedisValue> GetStringAsync(string key, CommandFlags flag = CommandFlags.None);
        /// <summary>
        /// 设置字符串数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = default, When when = When.Always, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// 加锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">Thread.CurrentThread.ManagedThreadId</param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task Lock(string key, int value, TimeSpan expiry);
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">Thread.CurrentThread.ManagedThreadId</param>
        /// <returns></returns>
        Task UnLock(string key, int value);

    }
}
