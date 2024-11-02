using BusinessObject;
using Repositories;
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
        public List<Pond> GetAll()
        {
            return _pondRepository.GetAll();    
        }
    }
}
