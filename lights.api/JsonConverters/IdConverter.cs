using System;
using System.ComponentModel;
using lights.api.Models;
using Newtonsoft.Json;

namespace lights.api.JsonConverters
{
    public class IdConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((Id)value).Value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Activator.CreateInstance(objectType, reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Id).IsAssignableFrom(objectType);
        }
    }
}
