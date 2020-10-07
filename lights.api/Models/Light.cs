using System;
using System.ComponentModel;
using lights.api.JsonConverters;
using lights.api.TypeConverters;
using Newtonsoft.Json;

namespace lights.api.Models
{
    [TypeConverter(typeof(IdTypeConverter<LightId>))]
    [JsonConverter(typeof(IdConverter))]
    public class LightId : Id
    {
        public LightId(string id)
            : base(id)
        {
        }
    }

    public class Light
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("state")]
        public LightState State { get; set; }
    }

    public class LightState
    {
        [JsonProperty("on")]
        public bool? On { get; set; }

        [JsonProperty("bri")]
        public int? Brightness { get; set; }

        [JsonProperty("bri_inc")]
        public int? BrightnessIncrement { get; set; }

        [JsonProperty("ct")]
        public int? ColorTemperature { get; set; }

        [JsonProperty("ct_inc")]
        public int? ColorTemperatureIncrement { get; set; }

        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("reachable")]
        public bool IsReachable { get; set; }

        [JsonProperty("scene")]
        public string SceneId { get; set; }

        [JsonProperty("transitiontime")]
        public int? TransitionTime { get; set; }
    }
}
