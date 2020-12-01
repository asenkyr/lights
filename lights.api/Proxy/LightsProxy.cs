﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Apis;
using lights.api.Models;
using lights.common.Configuration;
using Refit;

namespace lights.api.Proxy
{
    public class LightsProxy
    {
        private readonly ApplicationConfig _applicationConfig;
        private readonly ILightsApi _lightsApi;

        public LightsProxy(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig));
            _lightsApi = RestService.For<ILightsApi>(_applicationConfig.BridgeConfig.HueBridgeUri);
        }

        public async Task<Dictionary<LightId, Light>> GetLightsAsync()
        {
            return await _lightsApi.GetLights(_applicationConfig.BridgeConfig.UserName);
        }

        public async Task<Light> GetLightAsync(LightId id)
        {
            return await _lightsApi.GetLight(_applicationConfig.BridgeConfig.UserName, id);
        }

        public async Task SetStateAsync(LightId id, LightState state)
        {
            var response = await _lightsApi.SetState(_applicationConfig.BridgeConfig.UserName, id, state);
        }

        public async Task TurnOnAsync(LightId id)
        {
            await SetStateAsync(id, new LightState {On = true});
        }

        public async Task TurnOffAsync(LightId id)
        {
            await SetStateAsync(id, new LightState { On = false });
        }

        public async Task ToggleAsync(LightId id)
        {
            var currentLight = await _lightsApi.GetLight(_applicationConfig.BridgeConfig.UserName, id);
            var newState = new LightState {On = !currentLight.State.On};
            var response = await _lightsApi.SetState(_applicationConfig.BridgeConfig.UserName, id,
                newState);
        }

        public Task AdjustBrightness(LightId id, int increment, int transitionTime = 1)
        {
            return SetStateAsync(
                id, new LightState{BrightnessIncrement = increment, TransitionTime = transitionTime});
        }

        public Task AdjustTemperature(LightId id, int increment, int transitionTime = 1)
        {
            return SetStateAsync(
                id, new LightState { ColorTemperatureIncrement = increment, TransitionTime = transitionTime });
        }
    }
}
