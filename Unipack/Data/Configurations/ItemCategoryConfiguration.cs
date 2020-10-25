using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unipack.Models;

namespace Unipack.Data.Configurations
{
    public class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategory>
    {
        public void Configure(EntityTypeBuilder<ItemCategory> builder)
        {
            builder.HasKey(cat => cat.ItemCategoryID);

            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.AddedOn).HasColumnType("Date");

            builder.HasMany(c => c.Items).WithOne(i => i.Category);
        }
    }
}
