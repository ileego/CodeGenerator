using CodeGenerator.Infra.Common;
using CodeGenerator.Infra.Common.Helper;
using CodeGenerator.Infra.Common.ValueModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Infra.Common.Interfaces;

namespace CodeGenerator.Infra.Common.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ControllerExtension
    {
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static async Task<UserModel> CurrentUser(this ControllerBase controller)
        {
            var redis = ServiceLocator.Provider.GetService<ICache>();
            var jwtHelper = ServiceLocator.Provider.GetService<IJwtHelper>();
            var user = new CurrentUserHelper(redis, jwtHelper);
            return await user.GetUser();
        }

        /// <summary>
        /// 校验日期时间，为空时默认当天
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ValidateDate(this ControllerBase controller, DateTime? date)
        {
            return Convert.ToDateTime(null == date
                ? DateTime.Now.ToString("yyyy-MM-dd")
                : date.Value.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// 将ModelState验证的错误信息合并为使用split分隔的字符串
        /// </summary>
        /// <param name="state"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ToErrorString(this ModelStateDictionary state, string split = "||")
        {
            var errorInfo = new StringBuilder();
            foreach (var s in state.Values)
            {
                foreach (var p in s.Errors)
                {
                    errorInfo.AppendFormat("{0}{1}", p.ErrorMessage, split);
                }
            }

            if (errorInfo.Length > split.Length)
            {
                errorInfo.Remove(errorInfo.Length - 2, split.Length);
            }

            return errorInfo.ToString();
        }

        /// <summary>
        /// 将ModelState验证的错误信息转换为字符串列表
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static List<string> ToErrorList(this ModelStateDictionary state)
        {
            var errors = new List<string>();
            foreach (var s in state.Keys)
            {
                var entry = state.GetValueOrDefault(s);
                if (entry?.Errors != null && entry.Errors.Count > 0)
                {
                    foreach (var e in entry.Errors)
                    {
                        errors.Add($"{s}|{e.ErrorMessage}");
                    }
                }
            }

            return errors;
        }


        /// <summary>
        /// 服务实例
        /// </summary>
        public class ServiceLocator
        {
            public static IServiceProvider Provider { get; set; }

        }
    }
}