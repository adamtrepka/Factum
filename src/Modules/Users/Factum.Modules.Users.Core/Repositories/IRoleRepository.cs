﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Factum.Modules.Users.Core.Entities;

namespace Factum.Modules.Users.Core.Repositories;

internal interface IRoleRepository
{
    Task<Role> GetAsync(string name);
    Task<IReadOnlyList<Role>> GetAllAsync();
    Task AddAsync(Role role);
}