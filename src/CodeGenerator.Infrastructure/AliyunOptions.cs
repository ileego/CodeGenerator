using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Infrastructure
{
    /// <summary>
    /// 阿里云OSS配置
    /// </summary>
    public class AliyunOptions
    {
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }
        public string OssEndpoint { get; set; }
        public string StsEndpoint { get; set; }
        public string Bucket { get; set; }
        public string RoleArn { get; set; }
        public string RegionId { get; set; }
    }
}
