using CloudCityCakesMVC.Data.EntityConfiguration;
using CloudCityCakesMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudCityCakesMVC.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<CakeOrder> CakeOrders { get; set; }
        
        /// <summary>
        /// Used as part of FluentAPI to allow us to annotate POCO models with database specific attributes
        /// Each entity has its own configuration - we register each of those items in this class.
        /// Registrations can be found in the "EntityConfiguration" namespace of this assembly.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CakeOrderConfiguration.Configure(modelBuilder);
            UserConfiguration.Configure(modelBuilder);
        }
        
    }
}