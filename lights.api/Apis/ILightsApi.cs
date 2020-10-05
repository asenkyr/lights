using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface ILightsApi
    {
        [Get("/{username}/lights")]
        Task<Dictionary<string,Light>> GetLights(string username);

        [Get("/{username}/lights/{id}")]
        Task<Light> GetLight(string username, int id);

        [Put("/{username}/lights/{id}/state")]
        Task<string> SetState(string username, int id, [Body]LightState lightState);
    }
}
