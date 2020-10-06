using System;
using lights.api.Models;
using lights.common.Collections;
using Newtonsoft.Json;

namespace lights.api.JsonConverters
{

    public class OperatorConverter : JsonConverter<Operator>
    {
        private Map<Operator, string> _map = new Map<Operator, string>
        {
            {Operator.Equal, "eq"},
            {Operator.GreaterThan, "gt"},
            {Operator.LessThan, "lt"},
            {Operator.Value, "dx"},
            {Operator.DelayedValue, "ddx"},
            {Operator.Stable, "stable"},
            {Operator.NotStable, "not stable"},
            {Operator.In, "in"},
            {Operator.NotIn, "not in"},
        };

        public override void WriteJson(JsonWriter writer, Operator value, JsonSerializer serializer)
        {
            writer.WriteValue(_map.Forward(value));
        }

        public override Operator ReadJson(JsonReader reader, Type objectType, Operator existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return _map.Backward(reader.Value.ToString());
        }
    }
}
