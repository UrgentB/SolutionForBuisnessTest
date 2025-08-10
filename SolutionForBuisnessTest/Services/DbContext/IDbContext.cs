using Microsoft.EntityFrameworkCore;
using SolutionForBuisnessTest.Models;

namespace SolutionForBuisnessTest.Services.DbContext
{
    public interface IDbContext
    {
        public DbSet<DictionaryEntry> DictionaryEntry { get; set; }
        public DbSet<Resource> Resources { set; get; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<DocumentIncome> DocumentIncomes { get; set; }
        public DbSet<ResourceIncome> ResourceIncomes { get; set; }
        public void Save();
    }
}
