using BusinessObject;
using Repositories.FISH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FishService : IFishService
    {
        private readonly IFishRepository _fishRepository;
        public FishService()
        {
            _fishRepository = new FishRepository();
        }

        public void AddNewFish(Fish fish)
        {
            _fishRepository.AddNewFish(fish);
        }

        public void DeleteFish(int id)
        {
            _fishRepository.DeleteFish(id);
        }

        public List<Fish> GetAll(int memberId)
        {
            return _fishRepository.GetAll(memberId);
        }

        public void UpdateFish(Fish fish)
        {
            _fishRepository.UpdateFish(fish);
        }

        public List<Fish> GetFishByPondId(int pondId)
        {
            return _fishRepository.GetByPondId(pondId);
        }
        
        public int GetFishCount(int memberId)
        {
            return _fishRepository.GetFishCount(memberId);
        }
        
        public double GetAvgFishAge(int argPondId)
        {
            return _fishRepository.GetAvgFishAge(argPondId);
        }

        public decimal GetAvgFishSize(int argPondId)
        {
            return _fishRepository.GetAvgFishSize(argPondId);
        }
    }
}
