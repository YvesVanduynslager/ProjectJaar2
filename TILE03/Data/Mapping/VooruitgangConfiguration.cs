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
    public class VooruitgangConfiguration : IEntityTypeConfiguration<Vooruitgang>
    {
        public void Configure(EntityTypeBuilder<Vooruitgang> vooruitgang)
        {
            vooruitgang.HasKey(o => o.Id);
        }
    }
}