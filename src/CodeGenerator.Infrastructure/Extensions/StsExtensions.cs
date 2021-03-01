using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Auth.Sts;
using Aliyun.Acs.Core.Profile;
using Aliyun.OSS;
using Microsoft.Extensions.Options;

namespace CodeGenerator.Infrastructure.Extensions
{
    public class StsExtensions
    {
        private readonly DefaultAcsClient _client;
        private readonly AliyunOptions _aliyunOptions;

        public StsExtensions(IOptions<AliyunOptions> options)
        {
            _aliyunOptions = options.Value;
            // 构建一个阿里云 Client, 用于发起请求
            // 构建阿里云 Client 时需要设置 access key ID 和 access key secret
            //DefaultProfile.AddEndpoint(_aliyunOptions.RegionId, _aliyunOptions.RegionId, "Sts", _aliyunOptions.StsEndpoint);
            IClientProfile profile = DefaultProfile.GetProfile(_aliyunOptions.RegionId, _aliyunOptions.AccessKeyId, _aliyunOptions.AccessKeySecret);
            _client = new DefaultAcsClient(profile);
        }

        /// <summary>
        /// 所有权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public StsTokenModel GetFullAssumeRole(string userName)
        {
            // 构建 AssumeRole 请求
            AssumeRoleRequest request = new AssumeRoleRequest
            {
                //AcceptFormat = FormatType.JSON,
                RoleArn = _aliyunOptions.RoleArn, // 指定角色 ARN
                RoleSessionName = userName,
                DurationSeconds = 900, // 设置 token 有效期，可选参数，默认 900 秒；
                // 设置 token 的附加权限策略；在获取 token 时，通过额外设置一个权限策略进一步减小 Token 的权限；
                Policy = @"{
                              ""Statement"": [
                                {
                                  ""Action"": ""oss:*"",
                                  ""Effect"": ""Allow"",
                                  ""Resource"": ""*""
                                }
                              ],
                              ""Version"": ""1""
                            }"
            };

            AssumeRoleResponse response = _client.GetAcsResponse(request);
            return new StsTokenModel()
            {
                AccessKeyId = response.Credentials.AccessKeyId,
                AccessKeySecret = response.Credentials.AccessKeySecret,
                SecurityToken = response.Credentials.SecurityToken,
                Expiration = response.Credentials.Expiration
            };
        }
    }

    public class StsTokenModel
    {
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }
        public string SecurityToken { get; set; }
        public string Expiration { get; set; }
    }
}
