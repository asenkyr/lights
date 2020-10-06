using System.Collections.Generic;
using System.ComponentModel;
using lights.api.JsonConverters;
using lights.api.TypeConverters;
using Newtonsoft.Json;
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
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public SceneType Type{ get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("lights")]
        public int[] Lights { get; set; }

        [JsonProperty("lightstates")]
        public Dictionary<string, Light> LightStates { get; set; }

        [JsonProperty("appdata")]
        public JObject AppData { get; set; }
    }

    public enum SceneType
    {
        LightScene,
        GroupScene
    }
}
