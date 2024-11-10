using BusinessObject;
using Repositories;
using Repositories.POND;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PondService : IPondService
    {
        private readonly IPondRepository _pondRepository;
        public PondService()
        {
            _pondRepository = new PondRepository();
        }
        public List<Pond> GetAll(int memberId)
        {
            return _pondRepository.GetAll(memberId);    
        }
        public Pond GetById(int pondId)
        {
            return _pondRepository.GetById(pondId);
        }

        public void AddPond(Pond pond)
        {
            _pondRepository.AddPond(pond);
        }

        public void UpdatePond(Pond pond)
        {
            _pondRepository.UpdatePond(pond);
        }

        public void DeletePond(int pondId)
        {
            _pondRepository.DeletePond(pondId);
        }
    }
}
