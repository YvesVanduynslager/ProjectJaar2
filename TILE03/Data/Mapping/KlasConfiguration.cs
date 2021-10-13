using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TILE03.Models.Domain;

namespace TILE03.Data.Mapping
{
    public class KlasConfiguration : IEntityTypeConfiguration<Klas>
    {
        public void Configure(EntityTypeBuilder<Klas> klas)
        {
            klas.HasKey(k => k.Id);

            klas.HasMany(k => k.Leerlingen)
                .WithOne();
        }
    }
}