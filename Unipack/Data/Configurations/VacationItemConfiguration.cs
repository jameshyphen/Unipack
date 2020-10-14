using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unipack.Models;

namespace Unipack.Data.Configurations
{
    public class VacationItemConfiguration : IEntityTypeConfiguration<VacationItem>
    {
        public void Configure(EntityTypeBuilder<VacationItem> builder)
        {
            builder.HasKey(f => new { f.VacationItemId, f.ItemId });

            builder.HasOne(pt => pt.Item)
                .WithMany()
                .HasForeignKey(pt => pt.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.VacationList)
                .WithMany(p => p.Items)
                .HasForeignKey(pt => pt.VacationListId);
        }
    }
}
