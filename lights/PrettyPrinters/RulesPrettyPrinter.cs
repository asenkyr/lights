using System.Collections.Generic;
using System.Text;
using lights.api.Models;

namespace lights.PrettyPrinters
{
    public static class RulesPrettyPrinter
    {
        public static string BasicInfo(RuleRest groupRest)
        {
            return $"{groupRest.Name}";
        }

        public static string BasicInfo(IDictionary<string, RuleRest> groups)
        {
            var sb = new StringBuilder();
            foreach (var kvp in groups)
            {
                sb.AppendLine($"{kvp.Key} -- {BasicInfo(kvp.Value)}");
            }

            return sb.ToString();
        }
    }
}
