using System;
using System.Collections.Generic;
using System.Text;
using lights.api.Models;

namespace lights.api.PrettyPrinters
{
    public static class SensorsPrettyPrinter
    {
        public static string BasicInfo(Sensor sensor)
        {
            return $"{sensor.Name}";
        }

        public static string BasicInfo(IDictionary<string, Sensor> sensors)
        {
            var sb = new StringBuilder();
            foreach (var kvp in sensors)
            {
                sb.AppendLine($"{kvp.Key} -- {BasicInfo(kvp.Value)}");
            }

            return sb.ToString();
        }
    }
}
