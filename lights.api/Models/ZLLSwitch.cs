using Newtonsoft.Json;

namespace lights.api.Models
{
    public class ZLLSwitch : Sensor
    {
        [JsonProperty("config")]
        public ZLLSwitchConfig Config { get; set; }
    }

    public class ZLLSwitchConfig
    {
        [JsonProperty("on")]
        public bool IsOn { get; set; }

        [JsonProperty("battery")]
        public int BatteryLevel { get; set; }

        [JsonProperty("reachable")]
        public bool IsReachable { get; set; }
    }
}
