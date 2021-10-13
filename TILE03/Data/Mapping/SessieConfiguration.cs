using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TILE03.Models.Domain;

namespace TILE03.Data.Mapping
{
    public class SessieConfiguration : IEntityTypeConfiguration<Sessie>
    {
        public void Configure(EntityTypeBuilder<Sessie> sessie)
        {
            //builder.Ignore(s => s.SessieState)

            sessie.HasKey(s => s.Id);
            //sessie.Property(s => s.Id)
            //    .ValueGeneratedOnAdd();

            sessie.HasMany(s => s.Groepen)
                .WithOne();
            sessie.HasOne(s => s.Klas)
                .WithOne();
        }
    }
}