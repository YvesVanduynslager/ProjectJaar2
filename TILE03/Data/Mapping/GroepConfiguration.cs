using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TILE03.Models.Domain;

namespace TILE03.Data.Mapping
{
    public class GroepConfiguration : IEntityTypeConfiguration<Groep>
    {
        public void Configure(EntityTypeBuilder<Groep> groep)
        {
            groep.HasKey(g => g.Id);

            groep.HasMany(g => g.Leerlingen)
                .WithOne();
            groep.HasOne(g => g.UniekePad)
                .WithOne();
            groep.HasMany(g => g.Vooruitgang)
                .WithOne();
        }
    }
}