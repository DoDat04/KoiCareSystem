using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FISH
{
    public interface IFishService
    {
        List<Fish> GetAll(int memberId);
        List<Fish> GetFishByPondId(int pondId);
        public void AddNewFish(Fish fish);
        public void UpdateFish(Fish fish);
        public void DeleteFish(int id);

        public int GetFishCount(int memberId);
        double GetAvgFishAge(int argPondId);
        decimal GetAvgFishSize(int argPondId);

    }
}
