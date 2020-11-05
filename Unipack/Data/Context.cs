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
        public DbSet<User> UnipackUsers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<VacationItem> VacationItems { get; set; }
        public DbSet<VacationList> VacationLists { get; set; }
        public DbSet<VacationLocation> VacationLocations { get; set; }
        public DbSet<VacationTask> VacationTasks { get; set; }

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
            builder.Entity<Vacation>();

            builder.Entity<VacationItem>();

            builder.Entity<VacationList>();
            builder.Entity<VacationLocation>();
            builder.Entity<VacationTask>();

        }
    }
}
