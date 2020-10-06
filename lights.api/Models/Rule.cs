using System;
using System.ComponentModel;
using lights.api.JsonConverters;
using lights.api.TypeConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lights.api.Models
{
    [TypeConverter(typeof(IdTypeConverter<RuleId>))]
    [JsonConverter(typeof(IdConverter))]
    public class RuleId : Id
    {
        public RuleId(string id)
            : base(id)
        {
        }
    }

    public class Rule
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public DateTime TimeCreated{ get; set; }

        [JsonProperty("timestriggered")]
        public int TimesTriggered { get; set; }

        [JsonProperty("status")]
        public string Enabled { get; set; }
    }

    public class ConditionRest
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("operator")]
        public Operator Operator { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    [JsonConverter(typeof(OperatorConverter))]
    public enum Operator
    {
        Equal,
        GreaterThan,
        LessThan,
        Value,
        DelayedValue,
        Stable,
        NotStable,
        In,
        NotIn
    }

    public class ActionRest
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("body")]
        public JObject body { get; set; }
    }
}
