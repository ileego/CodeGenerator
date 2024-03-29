﻿using Aliyun.OSS;
using Microsoft.Extensions.Options;
using System;
using CodeGenerator.Infra.Common.Options;

namespace CodeGenerator.Infra.Common.Extensions
{
    public class OssExtensions
    {
        private readonly OssClient _ossClient;

        public OssExtensions(IOptions<AliyunOptions> options)
        {
            var aliyunOptions = options.Value;
            _ossClient = new OssClient(aliyunOptions.OssEndpoint, aliyunOptions.AccessKeyId, aliyunOptions.AccessKeySecret);
        }

        /// <summary>
        /// 获取上传Url签名
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public string GetUploadSignedUrl(string bucketName, string objectName)
        {
            // 生成上传签名URL。
            var request = new GeneratePresignedUriRequest(bucketName, objectName, SignHttpMethod.Put)
            {
                Expiration = DateTime.Now.AddMinutes(20),
            };
            var signedUrl = _ossClient.GeneratePresignedUri(request);

            // 返回签名URL。
            return signedUrl.ToString();
        }

        /// <summary>
        /// 获取下载Url
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public string GetDownloadSignedUrl(string bucketName, string objectName)
        {
            // 生成下载签名URL。
            var req = new GeneratePresignedUriRequest(bucketName, objectName, SignHttpMethod.Get);
            var uri = _ossClient.GeneratePresignedUri(req);

            // 返回下载Uri。
            return uri.ToString();
        }

    }
}
