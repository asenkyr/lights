using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Apis;
using lights.api.Models;
using lights.common.Configuration;
using Refit;

namespace lights.api.Proxy
{
    public class GroupsProxy
    {
        private readonly ApplicationConfig _applicationConfig;
        private readonly IGroupsApi _groupsApi;

        public GroupsProxy(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig));
            _groupsApi = RestService.For<IGroupsApi>(_applicationConfig.BridgeConfig.HueBridgeUri);
        }

        public async Task<Dictionary<GroupId, Group>> GetGroupsAsync()
        {
            return await _groupsApi.GetGroups(_applicationConfig.BridgeConfig.UserName);
        }

        public async Task<Group> GetGroupAsync(GroupId id)
        {
            return await _groupsApi.GetGroup(_applicationConfig.BridgeConfig.UserName, id);
        }

        public Task TurnOnAsync(GroupId id)
        {
            return _groupsApi.SetState(_applicationConfig.BridgeConfig.UserName, id,
                new LightState { On = true });
        }

        public Task TurnOffAsync(GroupId id)
        {
            return _groupsApi.SetState(_applicationConfig.BridgeConfig.UserName, id,
                new LightState { On = false });
        }

        public Task SetScene(GroupId id, SceneId sceneId)
        {
            return _groupsApi.SetState(_applicationConfig.BridgeConfig.UserName, id,
                new LightState {SceneId = sceneId});
        }
    }
}
