using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CodeGenerator.Infra.Common.Utils
{
    public static class RandomString
    {
        /// <summary>  
        /// 该方法用于生成指定位数的随机数  
        /// </summary>
        /// <param name="length">参数是随机数的位数</param>  
        /// <returns>返回一个随机数字符串</returns>  
        public static string Generate(int length)
        {
            //验证码可以显示的字符集合  
            var chars = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
            var array = chars.Split(new[] { ',' }); //拆分成数组   
            var codes = new StringBuilder(); //产生的随机数  
            var temp = -1; //记录上次随机数值，尽量避避免生产几个一样的随机数  

            //采用一个简单的算法以保证生成随机数的不同  
            for (var i = 1; i < length + 1; i++)
            {
                var t = RandomIndex(i, chars.Length); //获取随机数  
                if (temp != -1 && temp == t)
                {
                    t = RandomIndex(i, chars.Length);  //如果获取的随机数与上一次重复，则再生成一次  
                }

                temp = t; //把本次产生的随机数记录起来  
                codes.Append(array[t]); //随机数的位数加一  
            }

            return codes.ToString();
        }

        private static int RandomIndex(int pos, int length)
        {
            var random = new Random(pos * unchecked((int)DateTime.Now.Ticks)); //初始化随机类
            pos = random.Next(length);
            return pos;
        }
    }
}

