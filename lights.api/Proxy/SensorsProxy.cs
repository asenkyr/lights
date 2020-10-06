using lights.api.Apis;
using lights.common.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Proxy
{
    public class SensorsProxy
    {
        private readonly ApplicationConfig _applicationConfig;
        private readonly ISensorsApi _sensorsApi;

        public SensorsProxy(ApplicationConfig appConfig)
        {
            _applicationConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig));
            _sensorsApi = RestService.For<ISensorsApi>(_applicationConfig.BridgeConfig.HueBridgeUri);
        }

        public async Task<Dictionary<SensorId, Sensor>> GetSensorsAsync()
        {
            return await _sensorsApi.GetSensors(_applicationConfig.BridgeConfig.UserName);
        }

        public async Task<Sensor> GetSensorAsync(int id)
        {
            return await _sensorsApi.GetSensor(_applicationConfig.BridgeConfig.UserName, id);
        }
    }
}
