using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FishRepository : IFishRepository
    {
        public void AddNewFish(Fish fish)
        {
            using var _dbContext = new KoiCareContext();
            _dbContext.Add(fish);
            _dbContext.SaveChanges();
        }

        public List<Fish> GetAll()
        {
            using var _dbContext = new KoiCareContext();
            return _dbContext.Fish.ToList();
        }
    }
}
