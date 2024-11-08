using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Services
{
    public interface IWaterParamService
    {
        List<WaterParameter> GetAll(int memberId);
        WaterParameter? GetLatestByPondId(int pondId);
        void AddNewWaterParameter(WaterParameter waterParameter);
        void UpdateWaterParameter(WaterParameter waterParameter);
        void DeleteWaterParameter(int parameterId);
    }
}
