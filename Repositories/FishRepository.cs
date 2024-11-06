using BusinessObject;
using Microsoft.EntityFrameworkCore;
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

        public void DeleteFish(int id)
        {
            using var _dbContext = new KoiCareContext();
            var existingFish = _dbContext.Fish.Find(id);
            if (existingFish != null)
            {
                _dbContext.Remove(existingFish);
                _dbContext.SaveChanges();
            }          
        }

        public List<Fish> GetAll(int memberId)
        {
            using var _dbContext = new KoiCareContext();
            return _dbContext.Fish
                .Include(x => x.Pond)
                .Where(x => x.MemberId == memberId)
                .ToList();
        }

        public void UpdateFish(Fish fish)
        {
            using var _dbContext = new KoiCareContext();
            var existingFish = _dbContext.Fish.Find(fish.FishId);

            if (existingFish != null)
            {
                existingFish.Name = fish.Name;
                existingFish.PondId = fish.PondId;
                existingFish.Length = fish.Length;
                existingFish.Weight = fish.Weight;
                existingFish.BirthDate = fish.BirthDate;
                existingFish.Gender = fish.Gender;
                existingFish.Breed = fish.Breed;

                _dbContext.SaveChanges(); 
            }
        }

        public List<Fish> GetByPondId(int pondId)
        {
            using var _dbContext = new KoiCareContext();
            return _dbContext.Fish
                .Where(x => x.PondId == pondId)
                .ToList();
        }
    }
}
