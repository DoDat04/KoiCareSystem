using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Repositories;

namespace Services
{
    public class WaterParamService : IWaterParamService
    {
        private readonly IWaterParamRepository _waterParamRepository;

        public WaterParamService()
        {
            _waterParamRepository = new WaterParamRepository();
        }

        public List<WaterParameter> GetAll(int memberId)
        {
            return _waterParamRepository.GetAll(memberId);
        }

        public WaterParameter? GetLatestByPondId(int pondId)
        {
            return _waterParamRepository.GetLatestByPondId(pondId);
        }

        public void AddNewWaterParameter(WaterParameter waterParameter)
        {
            _waterParamRepository.AddNewWaterParameter(waterParameter);
        }

        public void UpdateWaterParameter(WaterParameter waterParameter)
        {
            _waterParamRepository.UpdateWaterParameter(waterParameter);
        }

        public void DeleteWaterParameter(int parameterId)
        {
            _waterParamRepository.DeleteWaterParameter(parameterId);
        }
    }
}
