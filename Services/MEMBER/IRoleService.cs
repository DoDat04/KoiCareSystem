﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MEMBER
{
    public interface IRoleService
    {
        List<Role> GetRoles();
    }
}
