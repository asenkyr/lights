using System;
using System.ComponentModel;
using lights.api.JsonConverters;
using lights.api.TypeConverters;
using Newtonsoft.Json;

namespace lights.api.Models
{
    [TypeConverter(typeof(IdTypeConverter<SensorId>))]
    [JsonConverter(typeof(IdConverter))]
    public class SensorId : Id
    {
        public SensorId(string id)
            : base(id)
        {
        }
    }

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
