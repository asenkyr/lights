using Newtonsoft.Json;

namespace lights.api.Models
{
    public class Group
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lights")]
        public string[] Lights{ get; set; }

        [JsonProperty("type")]
        public GroupType Type { get; set; }
    }

    public enum GroupType
    {
        System = 0,
        Luminaire,
        Lightsource,
        LightGroup,
        Room,
        Entertainment,
        Zone
    }
}
