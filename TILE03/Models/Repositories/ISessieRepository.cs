using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public interface ISessieRepository
    {
        ICollection<Sessie> GetAll();
        Sessie GetById(int id);
        Sessie GetByCode(string code);
        void Remove(Sessie sessie);
        void Add(Sessie sessie);
        void SaveChanges();
    }
}