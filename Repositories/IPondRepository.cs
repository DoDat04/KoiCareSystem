using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPondRepository
    {
        List<Pond> GetAll(int memberId);
        Pond GetById(int pondId);
        void AddPond(Pond pond);
        void UpdatePond(Pond pond);
        void DeletePond(int pondId);
    }
}
