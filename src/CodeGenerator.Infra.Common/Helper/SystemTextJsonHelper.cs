using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace CodeGenerator.Infra.Common.Helper
{
    public class SystemTextJsonHelper
    {
        public static JsonSerializerOptions GetDefaultOptions()
        {
            return new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                ,
                Encoder = GetDefaultEncoder()
                ,
                //该值指示是否允许、不允许或跳过注释
                ReadCommentHandling = JsonCommentHandling.Skip
                ,
                //dynamic与匿名类型序列化设置
                PropertyNameCaseInsensitive = true
                ,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public static JavaScriptEncoder GetDefaultEncoder()
        {
            return JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All));
        }
    }
}
