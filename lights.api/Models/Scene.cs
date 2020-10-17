using System.Collections.Generic;
using System.ComponentModel;
using lights.api.JsonConverters;
using lights.api.TypeConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace lights.api.Models
{
    [TypeConverter(typeof(IdTypeConverter<SceneId>))]
    [JsonConverter(typeof(IdConverter))]
    public class SceneId : Id
    {
        public SceneId(string id)
            : base(id)
        {
        }
    }

    public class Scene
    {
        [JsonProperty("id")]
        public SceneId Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public SceneType Type{ get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("lights")]
        public LightId[] Lights { get; set; }

        [JsonProperty("lightstates")]
        public Dictionary<LightId, LightState> LightStates { get; set; }

        [JsonProperty("appdata")]
        public JObject AppData { get; set; }

        [JsonProperty("recycle")] 
        public bool? Recycle { get; set; } = false;
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SceneType
    {
        LightScene,
        GroupScene
    }
}
