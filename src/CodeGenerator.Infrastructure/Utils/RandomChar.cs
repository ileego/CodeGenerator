using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CodeGenerator.Infrastructure.Utils
{
    public static class RandomChar
    {
        /// <summary>  
        /// 该方法用于生成指定位数的随机数  
        /// </summary>
        /// <param name="codeLength">参数是随机数的位数</param>  
        /// <returns>返回一个随机数字符串</returns>  
        public static string Generate(int codeLength)
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
                    return Generate(codeLength); //如果获取的随机数重复，则递归调用  
                }

                temp = t; //把本次产生的随机数记录起来  
                codes += array[t]; //随机数的位数加一  
            }

            return codes;
        }
    }
}
}
