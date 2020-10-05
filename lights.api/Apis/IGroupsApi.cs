﻿using System.Collections.Generic;
using System.Threading.Tasks;
using lights.api.Models;
using Refit;

namespace lights.api.Apis
{
    public interface IGroupsApi
    {
        [Get("/{username}/groups")]
        Task<Dictionary<string, Group>> GetGroups(string username);

        [Get("/{username}/groups/{id}")]
        Task<Group> GetGroup(string username, int id);
    }
}