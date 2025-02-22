﻿using BusinessObject;
using Repositories.MEMBER;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MEMBER
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository iRoleRepository;
        public RoleService()
        {
            iRoleRepository = new RoleRepository();
        }
        public List<Role> GetRoles()
        {
            return iRoleRepository.GetRoles();
        }
    }
}
