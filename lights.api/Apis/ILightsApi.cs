using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface ILightsApi
    {
        [Get("/{username}/lights")]
        Task<Dictionary<LightId, Light>> GetLights(string username);

        [Get("/{username}/lights/{id}")]
        Task<Light> GetLight(string username, LightId id);

        [Put("/{username}/lights/{id}/state")]
        Task<string> SetState(string username, LightId id, [Body]LightState lightState);
    }
}
