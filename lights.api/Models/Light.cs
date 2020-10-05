using Newtonsoft.Json;

namespace lights.api.Models
{
    public class Light
    {
        [JsonProperty("uniqueid")]
        public string Uid { get; set; }

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

        [JsonProperty("ct")]
        public int? ColorTemperature { get; set; }

        [JsonProperty("alert")]
        public string Alert { get; set; }

        [JsonProperty("reachable")]
        public bool IsReachable { get; set; }
    }
}
