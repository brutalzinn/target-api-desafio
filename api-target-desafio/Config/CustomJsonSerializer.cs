using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api_target_desafio.Config
{
    //THANKS TO https://stackoverflow.com/questions/58102189/formatting-datetime-in-asp-net-core-3-0-using-system-text-json
    //NOW DATETIME ARE RUNNING ON CORRECT FORMAT.
    public class CustomJsonSerializer : JsonConverter<DateTime>
    {
        
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.Parse(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
            }
        
    }
}
