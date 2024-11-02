using BusinessObject;
using Repositories;
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

        public List<Fish> GetAll()
        {
            return _fishRepository.GetAll();
        }
    }
}
