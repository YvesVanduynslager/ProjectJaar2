using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TILE03.Data;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public class UniekPadRepository : IUniekPadRepository
    {
        private DbSet<UniekPad> _uniekePaden;
        private ApplicationDbContext _dbContext;

        public UniekPadRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _uniekePaden = _dbContext.UniekePaden;
        }

        public ICollection<UniekPad> GetAll()
        {
            return _uniekePaden
                .Include(p => p.Opdrachten)
                .Include(p => p.Acties)
                .ToList();
        }

        public UniekPad GetById(int id)
        {
            return _uniekePaden
                .Include(p => p.Opdrachten)
                .Include(p => p.Acties)
                .SingleOrDefault(p => p.Id == id);
        }

        public void Remove(UniekPad pad)
        {
            _dbContext.UniekePaden.Remove(pad);
        }

        public void Add(UniekPad pad)
        {
            _dbContext.UniekePaden.Add(pad);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}