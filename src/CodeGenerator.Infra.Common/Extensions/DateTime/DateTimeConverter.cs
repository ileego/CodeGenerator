using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace System.Text.Json.Serialization
{
    public class DateTimeConverter : JsonConverter<System.DateTime>
    {
        public override System.DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return System.DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, System.DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
