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
        private readonly IRulesApi _rulesApi;

        public RulesProxy(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig));
            _rulesApi = RestService.For<IRulesApi>(_applicationConfig.BridgeConfig.HueBridgeUri);
        }

        public async Task<Dictionary<RuleId, Rule>> GetRulesAsync()
        {
            return await _rulesApi.GetRules(_applicationConfig.BridgeConfig.UserName);
        }

        public async Task<Rule> GetRuleAsync(RuleId id)
        {
            return await _rulesApi.GetRule(_applicationConfig.BridgeConfig.UserName, id);
        }
    }
}
