using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public interface IGroepRepository
    {
        ICollection<Groep> GetAll();
        Groep GetById(int id);
        void Remove(Groep groep);
        void Add(Groep groep);
        void Update(Groep groep);
        void SaveChanges();
    }
}