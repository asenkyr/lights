using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace lights.api.Models
{
    public class RuleRest
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
        public string Operator { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
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
