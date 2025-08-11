using Microsoft.EntityFrameworkCore;
using SolutionForBuisnessTest.Models;

namespace SolutionForBuisnessTest.Services.Validation
{
    public static class ValidationDictionaryEntryExtension 
    {
        public static string ResourceUsedByResourceIncome(this ValidationDictionaryEntry<Resource> validation, Guid id, DbSet<ResourceIncome> resourceIncomes)
        {
            return resourceIncomes.Any(x => x.Resource.Id == id)
                ? "Ресурс зайдейстован и не может быть удалён."
                : String.Empty;
        }

        public static string UnitUsedByResourceIncome(this ValidationDictionaryEntry<Unit> validation, Guid id, DbSet<ResourceIncome> resourceIncomes)
        {
            return resourceIncomes.Any(x => x.Unit.Id == id)
                ? "Единица измерения зайдейстована и не может быть удалён."
                : String.Empty;
        }
    }
}
