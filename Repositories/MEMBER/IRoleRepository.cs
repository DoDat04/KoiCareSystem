﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.MEMBER
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
    }
}
