using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infra.Common.Options
{
    /// <summary>
    /// Consul 配置
    /// </summary>
    public class ConsulOption
    {
        /// <summary>
        /// Consul Client 地址
        /// </summary>
        public string ConsulUrl { get; set; }
        /// <summary>
        /// //当前服务名称，可以多个实例共享
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// //当前服务地址
        /// </summary>
        public string ServiceUrl { get; set; }
        /// <summary>
        /// 健康检查的地址，当前服务公布出来的一个api接口
        /// </summary>
        public string HealthCheckUrl { get; set; }
        /// <summary>
        /// 健康检查心跳间隔
        /// </summary>
        public int HealthCheckIntervalInSecond { get; set; }
        /// <summary>
        /// 服务tag
        /// </summary>
        public string[] ServerTags { get; set; }
        /// <summary>
        /// //Key路径
        /// </summary>
        public string ConsulKeyPath { get; set; }
    }
}
