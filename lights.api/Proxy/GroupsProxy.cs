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
        private readonly IGroupsApi _lightsApi;

        public GroupsProxy(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig));
            _lightsApi = RestService.For<IGroupsApi>(_applicationConfig.BridgeConfig.HueBridgeUri);
        }

        public async Task<Dictionary<string, Group>> GetScenesAsync()
        {
            return await _lightsApi.GetGroups(_applicationConfig.BridgeConfig.UserName);
        }

        public async Task<Group> GetSceneAsync(int id)
        {
            return await _lightsApi.GetGroup(_applicationConfig.BridgeConfig.UserName, id);
        }
    }
}
