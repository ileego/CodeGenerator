using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using CodeGenerator.Infra.Common.Extensions;

namespace CodeGenerator.Infra.Common.Utils
{
    public class VerificationCode
    {

        /// <summary>  
        /// 该方法是将生成的随机数写入图像文件  
        /// </summary>  
        /// <param name="code">code是一个随机数</param>
        /// <param name="numbers">生成位数（默认4位）</param>
        public static MemoryStream Create(out string code, int numbers = 4)
        {
            code = Generate(numbers);
            Bitmap img = null;
            Graphics graphics = null;
            MemoryStream ms = null;
            var random = new Random();
            //验证码颜色集合  
            Color[] colors =
            {
                Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple,
                Color.SteelBlue, Color.CornflowerBlue, Color.CadetBlue, Color.OliveDrab, Color.OrangeRed, Color.DarkGreen, Color.DarkSlateGray,
                Color.Chocolate, Color.Indigo, Color.IndianRed, Color.LightSeaGreen, Color.YellowGreen, Color.MediumSlateBlue,
            };

            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };


            //定义图像的大小，生成图像的实例  
            img = new Bitmap((int)code.Length * 21, 46);

            graphics = Graphics.FromImage(img);//从Img对象生成新的Graphics对象    

            graphics.Clear(Color.White);//背景设为白色  

            //在随机位置画背景点  
            for (var i = 0; i < 100; i++)
            {
                var x = random.Next(img.Width);
                var y = random.Next(img.Height);
                graphics.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            //画图片的干扰线  
            for (int i = 0; i < 10; i++)
            {
                int x1 = random.Next(img.Width);
                int x2 = random.Next(img.Width);
                int y1 = random.Next(img.Height);
                int y2 = random.Next(img.Height);
                graphics.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }

            //画图片的前景干扰点  
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(img.Width);
                int y = random.Next(img.Height);
                img.SetPixel(x, y, Color.FromArgb(random.Next()));

            }

            //验证码绘制在g中  
            for (var i = 0; i < code.Length; i++)
            {
                var cIndex = random.Next(colors.Length - 1);//随机颜色索引值  
                //var fIndex = random.Next(5);//随机字体索引值  
                var font = new Font("Arial", 24, (FontStyle.Bold | FontStyle.Italic));//字体  
                Brush brush = new SolidBrush(colors[cIndex]);//颜色  
                var ii = 4;
                if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                {
                    ii = 2;
                }
                graphics.DrawString(code.Substring(i, 1), font, brush, 3 + (i * 17), ii);//绘制一个验证字符  
            }
            ms = new MemoryStream();//生成内存流对象  
            img.Save(ms, ImageFormat.Png);//将此图像以Png图像文件的格式保存到流中  

            //回收资源  
            graphics.Dispose();
            img.Dispose();
            return ms;
        }

        /// <summary>  
        /// 该方法用于生成指定位数的随机数  
        /// </summary>
        /// <param name="length">参数是随机数的位数</param>  
        /// <returns>返回一个随机数字符串</returns>  
        public static string Generate(int length)
        {
            //验证码可以显示的字符集合  
            var chars = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y";
            var array = chars.Split(new[] { ',' }); //拆分成数组   
            var codes = new StringBuilder(); //产生的随机数  
            var temp = -1; //记录上次随机数值，尽量避避免生产几个一样的随机数  

            //采用一个简单的算法以保证生成随机数的不同  
            for (var i = 1; i < length + 1; i++)
            {
                var t = RandomIndex(i, array.Length); //获取随机数  
                if (temp != -1 && temp == t)
                {
                    t = RandomIndex(i, array.Length);  //如果获取的随机数与上一次重复，则再生成一次  
                }

                temp = t; //把本次产生的随机数记录起来  
                codes.Append(array[t]); //随机数的位数加一  
            }

            return codes.ToString();
        }

        private static int RandomIndex(int pos, int length)
        {
            var random = new Random(pos * unchecked((int)DateTime.Now.Ticks)); //初始化随机类
            pos = random.StrictNext(length);
            return pos;
        }
    }
}
