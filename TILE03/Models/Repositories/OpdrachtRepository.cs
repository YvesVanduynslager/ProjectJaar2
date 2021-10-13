using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TILE03.Data;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public class OpdrachtRepository : IOpdrachtRepository
    {
        private readonly DbSet<Opdracht> _opdrachten;
        private readonly ApplicationDbContext _dbContext;

        public OpdrachtRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _opdrachten = _dbContext.Opdrachten;
        }

        public ICollection<Opdracht> GetAll()
        {
            return _opdrachten
                .Include(o => o.Oefening)
                .Include(o => o.Toegangscode)
                .ToList();
        }

        public Opdracht GetById(int id)
        {
            return _opdrachten
                .Include(o => o.Oefening)
                .ThenInclude(oef => oef.GroepsBewerking)
                .Include(o => o.Oefening)
                .ThenInclude(oef => oef.Antwoord)
                .Include(o => o.Toegangscode)
                .SingleOrDefault(o => o.Id == id);
        }

        public Opdracht GetByCode(string code)
        {
            return _opdrachten.Include(o => o.Oefening)
                .Include(o => o.Toegangscode)
                .SingleOrDefault(o => o.Toegangscode.Code == code);
        }

        public Opdracht GetByVolgNr(int nr)
        {
            return _opdrachten.Include(o => o.Oefening)
                .Include(o => o.Toegangscode)
                .SingleOrDefault(o => o.VolgNr == nr);
        }

        public void Remove(Opdracht opdracht)
        {
            _opdrachten.Remove(opdracht);
        }

        public void Add(Opdracht opdracht)
        {
            _opdrachten.Add(opdracht);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}