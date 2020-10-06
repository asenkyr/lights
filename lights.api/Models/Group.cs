using System.ComponentModel;
using lights.api.JsonConverters;
using lights.api.TypeConverters;
using Newtonsoft.Json;

namespace lights.api.Models
{
    [TypeConverter(typeof(IdTypeConverter<GroupId>))]
    [JsonConverter(typeof(IdConverter))]
    public class GroupId : Id
    {
        public GroupId(string id)
            : base(id)
        {
        }
    }

    public class Group
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lights")]
        public LightId[] Lights{ get; set; }

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
