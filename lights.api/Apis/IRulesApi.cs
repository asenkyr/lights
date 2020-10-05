using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface IRulesApi
    {
        [Get("/{username}/rules")]
        Task<Dictionary<string, RuleRest>> GetRules(string username);

        [Get("/{username}/rules/{id}")]
        Task<RuleRest> GetRule(string username, int id);
    }
}
