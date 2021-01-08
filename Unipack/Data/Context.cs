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
        public DbSet<Category> Categories { get; set; }
        public DbSet<PackItem> PackItems { get; set; }
        public DbSet<PackList> PackLists { get; set; }
        public DbSet<VacationLocation> VacationLocations { get; set; }
        public DbSet<PackTask> VacationTasks { get; set; }

        public Context(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>();
            builder.Entity<Category>();
            builder.Entity<Item>();
            builder.Entity<Vacation>();

            builder.Entity<PackItem>();

            builder.Entity<PackList>();
            builder.Entity<VacationLocation>();
            builder.Entity<PackTask>();

        }
    }
}
