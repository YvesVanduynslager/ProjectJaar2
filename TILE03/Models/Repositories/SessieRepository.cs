using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TILE03.Data;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public class SessieRepository : ISessieRepository
    {
        private readonly DbSet<Sessie> _sessies;
        private readonly ApplicationDbContext _dbContext;

        public SessieRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _sessies = _dbContext.Sessies;
        }

        public ICollection<Sessie> GetAll()
        {
            return _sessies
                .Include(s => s.Groepen)
                .ThenInclude(g => g.Leerlingen)
                .Include(s => s.Groepen)
                .ThenInclude(g => g.Vooruitgang)
                .Include(s => s.Klas)
                .OrderBy(s => s.Naam)
                .ToList();
        }

        public Sessie GetById(int id)
        {
            return _sessies
                .Include(s => s.Groepen)
                .ThenInclude(g => g.Leerlingen)
                .Include(s => s.Groepen)
                .ThenInclude(g => g.Vooruitgang)
                .Include(s => s.Klas)
                .SingleOrDefault(s => s.Id == id);
        }

        public Sessie GetByCode(string code)
        {
            return _sessies
                .Include(s => s.Groepen)
                .ThenInclude(g => g.Leerlingen)
                .Include(s => s.Groepen)
                .ThenInclude(g => g.Vooruitgang)
                .Include(s => s.Klas)
                .SingleOrDefault(s => s.Code == code);
        }

        public void Remove(Sessie sessie)
        {
            _sessies.Remove(sessie);
        }

        public void Add(Sessie sessie)
        {
            _sessies.Add(sessie);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}