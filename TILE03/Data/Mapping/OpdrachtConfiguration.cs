using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TILE03.Models.Domain;

namespace TILE03.Data.Mapping
{
    public class OpdrachtConfiguration : IEntityTypeConfiguration<Opdracht>
    {
        public void Configure(EntityTypeBuilder<Opdracht> opdracht)
        {
            opdracht.HasKey(o => o.Id);

            opdracht.HasOne(o => o.Oefening)
                .WithMany();
            opdracht.HasOne(o => o.Toegangscode)
                .WithOne();
        }
    }
}