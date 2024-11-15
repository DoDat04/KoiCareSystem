using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.POND
{
    public class PondRepository : IPondRepository
    {
        public List<Pond> GetAll(int memberId)
        {
            using var _dbContext = new KoiCareContext();
            return _dbContext.Ponds
                .Where(x => x.MemberId == memberId)
                .ToList();
        }
        public Pond GetById(int pondId)
        {
            using var _dbContext = new KoiCareContext();
            return _dbContext.Ponds
                             .Include(p => p.Fish)
                             .FirstOrDefault(p => p.PondId == pondId);
        }

        public void AddPond(Pond pond)
        {
            using var _dbContext = new KoiCareContext();
            _dbContext.Ponds.Add(pond);
            _dbContext.SaveChanges();
        }

        public void UpdatePond(Pond pond)
        {
            if (pond == null)
            {
                Console.WriteLine("Cannot update: provided pond is null.");
                return;
            }

            using var _dbContext = new KoiCareContext();
            var existingPond = _dbContext.Ponds.Find(pond.PondId);

            if (existingPond == null)
            {
                Console.WriteLine("Pond not found for update.");
                return;
            }

            existingPond.Name = pond.Name;
            existingPond.Length = pond.Length;
            existingPond.Width = pond.Width;
            existingPond.Depth = pond.Depth;
            existingPond.IsActive = pond.IsActive;
            existingPond.ImagePath = pond.ImagePath;
            _dbContext.SaveChanges();
        }

        public void DeletePond(int pondId)
        {
            try
            {
                using var _dbContext = new KoiCareContext();
                var existingPond = _dbContext.Ponds
                    .Include(p => p.Fish)
                    .FirstOrDefault(p => p.PondId == pondId);

                if (existingPond == null)
                {
                    throw new Exception("Pond not found.");
                }

                // Check if pond has fish and throw a more specific exception
                if (existingPond.Fish != null && existingPond.Fish.Any())
                {
                    throw new Exception($"Cannot delete pond: This pond contains {existingPond.Fish.Count} fish. Please move all fish to another pond first.");
                }

                _dbContext.Ponds.Remove(existingPond);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Rethrow the exception to be handled by the calling method
                throw new Exception(ex.Message);
            }
        }
    }
}
