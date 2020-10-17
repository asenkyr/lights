using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface IScenesApi
    {
        [Get("/{username}/scenes")]
        Task<Dictionary<SceneId, Scene>> GetScenes(string username);

        [Get("/{username}/scenes/{id}")]
        Task<Scene> GetScene(string username, SceneId id);

        [Post("/{username}/scenes")]
        Task<string> CreateScene(string username, [Body]Scene scene);

        [Delete("/{username}/scenes/{id}")]
        Task DeleteScene(string username, SceneId id);

    }
}
