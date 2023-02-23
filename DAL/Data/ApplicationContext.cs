using DAL.Domain;
using DAL.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DAL.Data
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; } = null!;
        public DbSet<Trophy> Trophes { get; set; } = null!;
        public DbSet<TypeAnimal> TypeAnimals { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["TestConnectionString"]
                 .ConnectionString;
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityBase>().UseTpcMappingStrategy();
        }
    }
}
