using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lights.api.Apis;
using lights.api.Models;
using lights.common.Configuration;
using Refit;

namespace lights.api.Proxy
{
    public class ScenesProxy
    {
        private readonly ApplicationConfig _applicationConfig;
        private readonly IScenesApi _lightsApi;

        public ScenesProxy(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig));
            _lightsApi = RestService.For<IScenesApi>(_applicationConfig.BridgeConfig.HueBridgeUri);
        }

        public async Task<Dictionary<string, Scene>> GetScenesAsync()
        {
            return await _lightsApi.GetScenes(_applicationConfig.BridgeConfig.UserName);
        }

        public async Task<Scene> GetSceneAsync(int id)
        {
            return await _lightsApi.GetScene(_applicationConfig.BridgeConfig.UserName, id);
        }
    }
}
