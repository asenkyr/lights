using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface IRulesApi
    {
        [Get("/{username}/rules")]
        Task<Dictionary<RuleId, Rule>> GetRules(string username);

        [Get("/{username}/rules/{id}")]
        Task<Rule> GetRule(string username, RuleId id);
    }
}
