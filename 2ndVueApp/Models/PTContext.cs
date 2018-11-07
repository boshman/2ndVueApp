using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _2ndVueApp.Models
{
    public class PTContext : DbContext
    {
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Earner> Earners { get; set; }
        public DbSet<Earning> Earnings { get; set; }

        public PTContext(DbContextOptions<PTContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rule>()
                .Property(b => b.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<Rule>()
                .HasMany(eg => eg.Earnings)
                .WithOne(r => r.Rule);

            modelBuilder.Entity<Earner>()
                .Property(e => e.LastName)
                .HasMaxLength(100);
            modelBuilder.Entity<Earner>()
                .Property(e => e.FirstName)
                .HasMaxLength(100);
            modelBuilder.Entity<Earner>()
                .HasMany(eg => eg.Earnings)
                .WithOne(e => e.Earner);
        }

    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PTContext>
    {
        public PTContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<PTContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new PTContext(builder.Options);
        }
    }
}
