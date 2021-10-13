using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TILE03.Data;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public class VooruitgangRepository : IVooruitgangRepository
    {
        private readonly DbSet<Vooruitgang> _vooruitgang;
        private readonly ApplicationDbContext _dbContext;

        public VooruitgangRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _vooruitgang = _dbContext.Vooruitgang;
        }

        public void Add(Vooruitgang vooruitgang)
        {
            _vooruitgang.Add(vooruitgang);
        }

        public void Update(ICollection<Vooruitgang> vooruitgangList)
        {
            _vooruitgang.UpdateRange(vooruitgangList);
        }

        public ICollection<Vooruitgang> GetAll()
        {
            return _vooruitgang.OrderBy(v => v.VolgNr).ToList();
        }

        void IVooruitgangRepository.SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
