using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TILE03.Data.Mapping;
using TILE03.Models;
using TILE03.Models.Domain;

namespace TILE03.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Sessie> Sessies { get; set; }
        public DbSet<Groep> Groepen { get; set; }
        public DbSet<UniekPad> UniekePaden { get; set; }
        public DbSet<Opdracht> Opdrachten { get; set; }
        public DbSet<Oefening> Oefeningen { get; set; }
        public DbSet<Vooruitgang> Vooruitgang { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new SessieConfiguration());
            builder.ApplyConfiguration(new KlasConfiguration());
            builder.ApplyConfiguration(new GroepConfiguration());
            builder.ApplyConfiguration(new UniekPadConfiguration());
            builder.ApplyConfiguration(new OpdrachtConfiguration());
            builder.ApplyConfiguration(new GroepsbewerkingConfiguration());
            builder.ApplyConfiguration(new AntwoordConfiguration());
            builder.ApplyConfiguration(new OefeningConfiguration());
            builder.ApplyConfiguration(new VooruitgangConfiguration());
        }
    }
}