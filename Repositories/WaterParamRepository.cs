using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class WaterParamRepository : IWaterParamRepository
    {
        public List<WaterParameter> GetAll(int memberId)
        {
            using var context = new KoiCareContext();
            return context.WaterParameters
                .Include(w => w.Pond)
                .Where(w => w.Pond.MemberId == memberId && w.IsActive)
                .OrderByDescending(w => w.MeasurementDate)
                .ToList();
        }

        public WaterParameter? GetLatestByPondId(int pondId)
        {
            using var context = new KoiCareContext();
            return context.WaterParameters
                .Where(w => w.PondId == pondId && w.IsActive)
                .OrderByDescending(w => w.MeasurementDate)
                .FirstOrDefault();
        }

        public void AddNewWaterParameter(WaterParameter waterParameter)
        {
            using var context = new KoiCareContext();
            context.WaterParameters.Add(waterParameter);
            context.SaveChanges();
        }

        public void UpdateWaterParameter(WaterParameter waterParameter)
        {
            using var context = new KoiCareContext();
            context.WaterParameters.Update(waterParameter);
            context.SaveChanges();
        }

        public void DeleteWaterParameter(int parameterId)
        {
            using var context = new KoiCareContext();
            var waterParam = context.WaterParameters.Find(parameterId);
            if (waterParam != null)
            {
                waterParam.IsActive = false;
                context.SaveChanges();
            }
        }
    }
}
