using SolutionForBuisnessTest.Models;
using Microsoft.EntityFrameworkCore;

namespace SolutionForBuisnessTest.Services.Validation
{
    public class ValidationEntityWithIdentificationString<T>(DbSet<T> set): BaseValidation<T>(set) where T : class, IEntityWithIdentificationString
    {
        public virtual string AlreadyExists(string name)
        {
            return set.Any(x => x.IdentificationString == name) ? 
                $"Запись с таким именем/номером уже есть" 
                : string.Empty;
        }
    }
}
