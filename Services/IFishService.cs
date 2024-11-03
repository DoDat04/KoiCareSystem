using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IFishService
    {
        List<Fish> GetAll(int memberId);
        public void AddNewFish(Fish fish);
        public void UpdateFish(Fish fish);
        public void DeleteFish(int id);
    }
}
