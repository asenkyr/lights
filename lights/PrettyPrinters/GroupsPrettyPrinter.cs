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

        public static string BasicInfo(IDictionary<GroupId, Group> groups)
        {
            var sb = new StringBuilder();
            foreach (var kvp in groups)
            {
                sb.AppendLine($"{kvp.Key.Value} -- {BasicInfo(kvp.Value)}");
            }

            return sb.ToString();
        }

        private static string ArrayToString(LightId[] array)
        {
            var sb = new StringBuilder();
            foreach (var item in array)
            {
                sb.Append($"{item.Value} ");
            }

            return sb.ToString();
        }
    }
}
