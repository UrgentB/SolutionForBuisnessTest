using Microsoft.EntityFrameworkCore;
using SolutionForBuisnessTest.Models;

namespace SolutionForBuisnessTest.Services.DbContext
{
    public class BaseDbContext(IConfiguration config) : PostrgresContext(config), IDbContext
    {
        public DbSet<DictionaryEntry> DictionaryEntry { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<DocumentIncome> DocumentIncomes { get; set; }
        public DbSet<ResourceIncome> ResourceIncomes { get; set; }
        public void Save() => SaveChanges();
    }
}
