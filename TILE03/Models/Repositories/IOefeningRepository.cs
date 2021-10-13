using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public interface IOefeningRepository
    {
        ICollection<Oefening> GetAll();
        Oefening GetById(int id);
    }
}