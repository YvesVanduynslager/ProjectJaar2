using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TILE03.Data;
using TILE03.Models.Domain;

namespace TILE03.Models.Repositories
{
    public class GroepRepository : IGroepRepository
    {
        private readonly DbSet<Groep> _groepen;
        private readonly ApplicationDbContext _dbContext;

        public GroepRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _groepen = _dbContext.Groepen;
        }

        public ICollection<Groep> GetAll()
        {
            return _groepen
                .Include(g => g.Leerlingen)
                .Include(g => g.UniekePad).ThenInclude(u => u.Opdrachten).ThenInclude(o => o.Oefening)
                .Include(g => g.UniekePad).ThenInclude(u => u.Acties)
                .Include(g => g.Vooruitgang)
                .ToList();
        }

        public Groep GetById(int id)
        {
            return _groepen
                .Include(g => g.Leerlingen)
                .Include(g => g.UniekePad).ThenInclude(u => u.Opdrachten).ThenInclude(o => o.Oefening)
                .Include(g => g.UniekePad).ThenInclude(u => u.Acties)
                .Include(g => g.Vooruitgang)
                .SingleOrDefault(g => g.Id == id);
        }

        public void Remove(Groep groep)
        {
            _groepen.Remove(groep);
        }

        public void Add(Groep groep)
        {
            _groepen.Add(groep);
        }

        public void Update(Groep groep)
        {
            //aangeven aan context dat de state veranderd is en moet gepersisteerd worden
            _dbContext.Entry(groep).Property(o => o.HuidigeState).IsModified = true;

            //enkel PlaatsInReeks update, TotaalAantalOefening is niet nodig, want wijzigt normaal gezien niet
            _dbContext.Entry(groep).Property(o => o.PlaatsInReeks).IsModified = true;

            _dbContext.Entry(groep).Collection(o => o.Vooruitgang).IsModified = true;

            //hier geven we aan dat het uniekPad niet hoeft gepersisteerd te worden, anders teveel overhead
            //geeft anders ook een fout ivm groepsbewerking
            _dbContext.Entry(groep).Reference(o => o.UniekePad).IsModified = false;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}