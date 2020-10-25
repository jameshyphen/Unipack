using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unipack.Models;

namespace Unipack.Data
{
    public class Context : IdentityDbContext
    {
        public new DbSet<User> Users { get; set; }
        public new DbSet<Item> Items { get; set; }
        public new DbSet<ItemCategory> ItemCategories { get; set; }
        public new DbSet<VacationItem> VacationItems { get; set; }
        public new DbSet<VacationList> VacationLists { get; set; }
        public new DbSet<VacationLocation> VacationLocations { get; set; }
        public new DbSet<VacationTask> VacationTasks { get; set; }

        public Context(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>();
            builder.Entity<ItemCategory>();
            builder.Entity<Item>();

            builder.Entity<VacationItem>();

            builder.Entity<VacationList>();
            builder.Entity<VacationLocation>();
            builder.Entity<VacationTask>();

        }
    }
}
