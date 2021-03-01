using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeGenerator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "user_name_hello";
            var str2 = "USER_NAME_HELLO_WORLD";
            Console.WriteLine("The camel name is: " + ToCamel(str));
            Console.WriteLine("The pascal name is: " + ToPascal(str2));
        }

        public static string ToCamel(string input, char split = '_')
        {
            input = input.ToLower();
            var regex = new Regex(@"_(\w?)");
            var outString = "";
            if (regex.IsMatch(input))
            {
                outString = regex.Replace(input, m => m.Groups[1].ToString().ToUpper());
            }
            return outString;
        }
        public static string ToPascal(string input, char split = '_')
        {
            input = input.ToLower();
            var strArray = input.Split(split);
            var stringBuilder = new StringBuilder();
            foreach (var s in strArray)
            {
                stringBuilder.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s));
            }

            return stringBuilder.ToString();
        }
    }
}
