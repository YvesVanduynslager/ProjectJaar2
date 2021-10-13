using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public interface IVooruitgangRepository
    {
        ICollection<Vooruitgang> GetAll();
        void Add(Vooruitgang vooruitgang);
        void Update(ICollection<Vooruitgang> vooruitgangList);
        void SaveChanges();
    }
}
