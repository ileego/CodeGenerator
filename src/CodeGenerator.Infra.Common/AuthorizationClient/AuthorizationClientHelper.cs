using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.AuthorizationClient;
using CodeGenerator.Infra.Common.Options;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CodeGenerator.Infra.Common.Helper
{
    public class AuthorizationClientHelper : IAuthorizationClientHelper
    {
        private readonly AuthorizationClientOption _clientOption;
        public AuthorizationClientHelper(IOptions<AuthorizationClientOption> options)
        {
            _clientOption = options.Value;
        }

        /// <summary>
        /// 从授权服务获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AuthorizeResult<UserModel>> GetUser(string token)
        {
            var encryptToken = CryptoHelper.Des3Encrypt(token, _clientOption.Secret);
            var req = new RequestAuthorizeInfoModel()
            {
                Token = encryptToken,
                SoftwareId = _clientOption.SoftwareId
            };
            const string api = "/api/authorizeInfo/user";
            var url = $"{_clientOption.Host}{api}";
            var result = await PostAsync<AuthorizeResult<UserModel>, RequestAuthorizeInfoModel>(url, req);
            return result;
        }

        /// <summary>
        /// 从授权服务获取本软件功能，IsSelected为True表示已授权
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AuthorizeResult<ICollection<FunctionsQueryWithSelectedResult>>> GetFunctions(string token)
        {
            var encryptToken = CryptoHelper.Des3Encrypt(token, _clientOption.Secret);
            var req = new RequestAuthorizeInfoModel()
            {
                Token = encryptToken,
                SoftwareId = _clientOption.SoftwareId
            };
            const string api = "/api/authorizeInfo/functions";
            var url = $"{_clientOption.Host}{api}";
            var result = await PostAsync<AuthorizeResult<ICollection<FunctionsQueryWithSelectedResult>>, RequestAuthorizeInfoModel>(url, req);
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        public async Task<AuthorizeResult<bool>> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var encryptToken = CryptoHelper.Des3Encrypt(changePasswordModel.Token, _clientOption.Secret);
            changePasswordModel.Token = encryptToken;
            changePasswordModel.SoftwareId = _clientOption.SoftwareId;
            const string api = "/api/authorizeInfo/changePassword";
            var url = $"{_clientOption.Host}{api}";
            var result = await PostAsync<AuthorizeResult<bool>, ChangePasswordModel>(url, changePasswordModel);
            return result;
        }

        private async Task<TResult> PostAsync<TResult, TData>(
            string url,
            TData data,
            X509Certificate2 certificate2 = null,
            string token = null)
        {
            var httpClientHandler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual
            };

            if (certificate2 != null)
            {
                httpClientHandler.ClientCertificates.Add(certificate2);
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true;
            }

            using var httpClient = new HttpClient(httpClientHandler) { Timeout = TimeSpan.FromSeconds(20) };
            //httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("shit"));
            var jsonData = JsonConvert.SerializeObject(data);
            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var r = JsonConvert.DeserializeObject<TResult>(responseContent);
                return r;
            }
            else
            {
                throw new Exception($"请求授权服务失败，错误代码：{response.StatusCode}");
            }
        }


    }

}
