using Microsoft.EntityFrameworkCore;
using SolutionForBuisnessTest.Models;

namespace SolutionForBuisnessTest.Services.DbContext
{
    public class PostrgresContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private string connectionString;
        public PostrgresContext(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DictionaryEntry>().UseTpcMappingStrategy();
        }
    }
}
