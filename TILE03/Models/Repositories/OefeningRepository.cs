using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TILE03.Data;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public class OefeningRepository : IOefeningRepository
    {
        private readonly DbSet<Oefening> _oefeningen;
        private readonly ApplicationDbContext _dbContext;

        public OefeningRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _oefeningen = _dbContext.Oefeningen;
        }

        public ICollection<Oefening> GetAll()
        {
            return _oefeningen
                .Include(o => o.GroepsBewerking)
                .Include(o => o.Antwoord)
                .ToList();
        }

        public Oefening GetById(int id)
        {
            return _oefeningen
                .Include(o => o.GroepsBewerking)
                .Include(o => o.Antwoord)
                .SingleOrDefault(o => o.Id == id);
        }
    }
}