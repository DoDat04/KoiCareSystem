using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
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
            _dbContext.SaveChanges();
        }

        public void DeletePond(int pondId)
        {
            try
            {
                using var _dbContext = new KoiCareContext();
                var existingPond = _dbContext.Ponds.Find(pondId);

                if (existingPond == null)
                {
                    Console.WriteLine("Pond not found for deletion.");
                    return;
                }

                _dbContext.Ponds.Remove(existingPond);

                int changes = _dbContext.SaveChanges();
                if (changes > 0)
                {
                    Console.WriteLine("Pond deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Delete operation did not affect any rows. No changes saved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting pond: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}
