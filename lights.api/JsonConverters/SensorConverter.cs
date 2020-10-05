using System;
using System.Collections.Generic;
using System.Text;
using lights.api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lights.api.JsonConverters
{
    class SensorConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var sensorType = (string) jObject["type"];

            Sensor sensor;

            switch (sensorType)
            {
                case SensorTypes.ZLLSwitch:
                    sensor = new ZLLSwitch();
                    break;

                default:
                    sensor = new Sensor();
                    break;
            }

            serializer.Populate(jObject.CreateReader(), sensor);
            return sensor;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Sensor).IsAssignableFrom(objectType);
        }
    }
}
