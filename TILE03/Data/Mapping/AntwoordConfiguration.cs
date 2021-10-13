using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TILE03.Models.Domain;

namespace TILE03.Data.Mapping
{
    public class AntwoordConfiguration : IEntityTypeConfiguration<Antwoord>
    {
        public void Configure(EntityTypeBuilder<Antwoord> antwoord)
        {
            antwoord.HasKey(a => a.Id);
        }
    }
}