using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => i.ItemId);

            builder.Property(i => i.Name).IsRequired();
            builder.Property(i => i.AddedOn).HasColumnType("Date");

            builder.HasOne(i => i.Category).WithMany(c => c.Items).HasForeignKey(c => c.CategoryId);
        }
    }
}
