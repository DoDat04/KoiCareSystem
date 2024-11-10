using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.MEMBER
{
    public class RoleRepository : IRoleRepository
    {
        public List<Role> GetRoles()
        {
            var _dbContext = new KoiCareContext();
            List<Role> roles = new List<Role>();
            foreach (var role in _dbContext.Roles)
            {
                roles.Add(role);
            }
            return roles;
        }
    }
}
