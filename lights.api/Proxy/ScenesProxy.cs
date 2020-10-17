﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using lights.api.Apis;
using lights.api.Models;
using lights.common.Configuration;
using Refit;

namespace lights.api.Proxy
{
    public class ScenesProxy
    {
        private readonly ApplicationConfig _applicationConfig;
        private readonly IScenesApi _scenesApi;

        public ScenesProxy(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig));
            _scenesApi = RestService.For<IScenesApi>(_applicationConfig.BridgeConfig.HueBridgeUri);
        }

        public Task<Dictionary<SceneId, Scene>> GetScenesAsync()
        {
            return _scenesApi.GetScenes(_applicationConfig.BridgeConfig.UserName);
        }

        public Task<Scene> GetSceneAsync(SceneId id)
        {
            return _scenesApi.GetScene(_applicationConfig.BridgeConfig.UserName, id);
        }

        public Task<string> CreateScene(Scene scene)
        {
            return _scenesApi.CreateScene(_applicationConfig.BridgeConfig.UserName, scene);
        }

        public Task DeleteSceneAsync(SceneId id)
        {
            return _scenesApi.DeleteScene(_applicationConfig.BridgeConfig.UserName, id);
        }
    }
}
