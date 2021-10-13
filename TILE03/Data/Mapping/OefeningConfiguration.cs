using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TILE03.Models.Domain;

namespace TILE03.Data.Mapping
{
    public class OefeningConfiguration : IEntityTypeConfiguration<Oefening>
    {
        public void Configure(EntityTypeBuilder<Oefening> oefening)
        {
            oefening.HasKey(o => o.Id);

            oefening.HasOne(o => o.Antwoord)
                .WithOne();
            oefening.HasOne(o => o.GroepsBewerking)
                .WithMany();
        }
    }
}