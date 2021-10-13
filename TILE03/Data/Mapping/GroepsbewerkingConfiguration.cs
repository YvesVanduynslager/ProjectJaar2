using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TILE03.Models.Domain;
using TILE03.Models.Domain.BewerkingStrategy;

namespace TILE03.Data.Mapping
{
    public class GroepsbewerkingConfiguration : IEntityTypeConfiguration<GroepsBewerking>
    {
        public void Configure(EntityTypeBuilder<GroepsBewerking> bewerking)
        {
            bewerking.HasKey(b => b.Id);

            bewerking.HasDiscriminator<string>("Type")
                .HasValue<Vermenigvuldig>("Vermenigvuldig")
                .HasValue<Delen>("Delen")
                .HasValue<Optellen>("Optellen")
                .HasValue<Aftrekken>("Aftrekken");
        }
    }
}