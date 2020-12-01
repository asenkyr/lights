using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface IGroupsApi
    {
        [Get("/{username}/groups")]
        Task<Dictionary<GroupId, Group>> GetGroups(string username);

        [Get("/{username}/groups/{id}")]
        Task<Group> GetGroup(string username, GroupId id);

        [Put("/{username}/groups/{id}/action")]
        Task SetState(string username, GroupId id, [Body]LightState state);
    }
}
