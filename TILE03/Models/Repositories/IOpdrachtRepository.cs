using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public interface IOpdrachtRepository
    {
        ICollection<Opdracht> GetAll();
        Opdracht GetById(int id);
        Opdracht GetByCode(string code);
        Opdracht GetByVolgNr(int nr);
        void Remove(Opdracht opdracht);
        void Add(Opdracht opdracht);
        void SaveChanges();
    }
}