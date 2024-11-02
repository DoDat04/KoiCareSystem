using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PondRepository : IPondRepository
    {
        public List<Pond> GetAll()
        {
            using var _dbContext = new KoiCareContext();
            return _dbContext.Ponds.ToList();
        }
    }
}
