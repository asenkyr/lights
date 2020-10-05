﻿using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface IScenesApi
    {
        [Get("/{username}/scenes")]
        Task<Dictionary<string, Scene>> GetScenes(string username);

        [Get("/{username}/scenes/{id}")]
        Task<Scene> GetScene(string username, int id);
    }
}