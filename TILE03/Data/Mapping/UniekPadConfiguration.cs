using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TILE03.Models.Domain;

namespace TILE03.Data.Mapping
{
    public class UniekPadConfiguration : IEntityTypeConfiguration<UniekPad>
    {
        public void Configure(EntityTypeBuilder<UniekPad> uniekPad)
        {
            uniekPad.HasKey(up => up.Id);

            uniekPad.HasMany(up => up.Acties);
            uniekPad.HasMany(up => up.Opdrachten);
        }
    }
}