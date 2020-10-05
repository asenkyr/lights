using System;
using lights.api.JsonConverters;
using Newtonsoft.Json;

namespace lights.api.Models
{
    [JsonConverter(typeof(SensorConverter))]
    public class Sensor
    {
        [JsonProperty("uniqueid")]
        public string Uid { get; set; }

        [JsonProperty("name")]
        public string Name{ get; set; }

        [JsonProperty("state")]
        public SensorState State { get; set; }

        [JsonProperty("type")]
        public string Type{ get; set; }
    }

    public class SensorState
    {
        [JsonProperty("buttonevent")]
        public int ButtonEvent{ get; set; }

        [JsonProperty("lastupdated")]
        public DateTime LastUpdated { get; set; }
    }
}
