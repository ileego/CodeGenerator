using CodeGenerator.Infrastructure;
using CodeGenerator.Infrastructure.Helper;
using CodeGenerator.Infrastructure.ValueModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Infrastructure.Extensions
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
            var redis = ServiceLocator.Provider.GetService<IRedis>();
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
            if (null == date)
            {
                return Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                return Convert.ToDateTime(date.Value.ToString("yyyy-MM-dd"));
            }
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
                if (entry.Errors != null && entry.Errors.Count > 0)
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
        /// 该方法用于生成指定位数的随机数  
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="codeLength">参数是随机数的位数</param>  
        /// <returns>返回一个随机数字符串</returns>  
        public static string RandomChar(this ControllerBase controller, int codeLength)
        {
            //验证码可以显示的字符集合  
            var chars = "0,1,2,3,4,5,6,7,8,9";
            var array = chars.Split(new Char[] { ',' }); //拆分成数组   
            var codes = ""; //产生的随机数  
            var temp = -1; //记录上次随机数值，尽量避避免生产几个一样的随机数  

            var rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同  
            for (var i = 1; i < codeLength + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks)); //初始化随机类  
                }

                var t = rand.Next(array.Length); //获取随机数  
                if (temp != -1 && temp == t)
                {
                    return RandomChar(controller, codeLength); //如果获取的随机数重复，则递归调用  
                }

                temp = t; //把本次产生的随机数记录起来  
                codes += array[t]; //随机数的位数加一  
            }

            return codes;
        }
    }

    /// <summary>
    /// 服务实例
    /// </summary>
    public class ServiceLocator
    {
        public static IServiceProvider Provider { get; set; }

    }
}