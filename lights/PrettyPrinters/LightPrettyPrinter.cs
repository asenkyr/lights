using System.Collections.Generic;
using System.Text;
using lights.api.Models;

namespace lights.PrettyPrinters
{
    public static class LightPrettyPrinter
    {
        public static string BasicInfo(Light light)
        {
            return $"{GenericPrettyPrinter.CheckBox(light.State.On ?? false)} ({light.State.Brightness})\t{light.Name}";
        }

        public static string BasicInfo(IDictionary<string, Light> lights)
        {
            var sb = new StringBuilder();
            foreach (var kvp in lights)
            {
                sb.AppendLine($"{kvp.Key} -- {BasicInfo(kvp.Value)}");
            }

            return sb.ToString();
        }
    }
}
