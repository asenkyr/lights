using System.Collections.Generic;
using System.Text;
using lights.api.Models;

namespace lights.PrettyPrinters
{
    public static class RulesPrettyPrinter
    {
        public static string BasicInfo(Rule @group)
        {
            return $"{@group.Name}";
        }

        public static string BasicInfo(IDictionary<RuleId, Rule> groups)
        {
            var sb = new StringBuilder();
            foreach (var kvp in groups)
            {
                sb.AppendLine($"{kvp.Key.Value} -- {BasicInfo(kvp.Value)}");
            }

            return sb.ToString();
        }
    }
}
