using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public interface IUniekPadRepository
    {
        ICollection<UniekPad> GetAll();
        UniekPad GetById(int id);
        void Remove(UniekPad pad);
        void Add(UniekPad pad);
        void SaveChanges();
    }
}