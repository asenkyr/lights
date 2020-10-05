using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface ISensorsApi
    {
        [Get("/{username}/sensors")]
        Task<Dictionary<string, Sensor>> GetSensors(string username);

        [Get("/{username}/sensors/{id}")]
        Task<Sensor> GetSensor(string username, int id);
    }
}
