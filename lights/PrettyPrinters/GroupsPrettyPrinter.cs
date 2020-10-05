using System.Collections.Generic;
using System.Text;
using lights.api.Models;

namespace lights.PrettyPrinters
{
    public static class GroupsPrettyPrinter
    {
        public static string BasicInfo(Group group)
        {
            return $"{group.Name} Type:{group.Type}, Lights:{ArrayToString(group.Lights)}";
        }

        public static string BasicInfo(IDictionary<string, Group> groups)
        {
            var sb = new StringBuilder();
            foreach (var kvp in groups)
            {
                sb.AppendLine($"{kvp.Key} -- {BasicInfo(kvp.Value)}");
            }

            return sb.ToString();
        }

        private static string ArrayToString(string[] array)
        {
            var sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.Append($"{item} ");
            }

            return sb.ToString();
        }
    }
}
