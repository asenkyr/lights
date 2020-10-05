using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Apis;
using lights.api.Models;
using lights.common.Configuration;
using Refit;

namespace lights.api.Proxy
{
    public class RulesProxy
    {
        private readonly ApplicationConfig _applicationConfig;
        private readonly IRulesApi _lightsApi;

        public RulesProxy(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig));
            _lightsApi = RestService.For<IRulesApi>(_applicationConfig.BridgeConfig.HueBridgeUri);
        }

        public async Task<Dictionary<string, RuleRest>> GetScenesAsync()
        {
            return await _lightsApi.GetRules(_applicationConfig.BridgeConfig.UserName);
        }

        public async Task<RuleRest> GetSceneAsync(int id)
        {
            return await _lightsApi.GetRule(_applicationConfig.BridgeConfig.UserName, id);
        }
    }
}
