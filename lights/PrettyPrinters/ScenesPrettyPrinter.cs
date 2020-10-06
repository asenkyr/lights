using System.Collections.Generic;
using System.Linq;
using System.Text;
using lights.api.Models;

namespace lights.PrettyPrinters
{
    public static class ScenesPrettyPrinter
    {
        public static string BasicInfo(Scene scene)
        {
            return $"{scene.Name}, Group:{scene.Group}, Lights:{ArrayToString(scene.Lights)}";
        }

        public static string BasicInfo(IDictionary<SceneId, Scene> scenes)
        {
            var sb = new StringBuilder();
            foreach (var kvp in scenes)
            {
                sb.AppendLine($"{kvp.Key.Value} -- {BasicInfo(kvp.Value)}");
            }

            return sb.ToString();
        }

        private static string ArrayToString(int[] array)
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
