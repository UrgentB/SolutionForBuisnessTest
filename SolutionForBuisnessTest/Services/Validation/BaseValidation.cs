using Microsoft.EntityFrameworkCore;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services.DbContext;

namespace SolutionForBuisnessTest.Services.Validation
{
    public class BaseValidation<T>(DbSet<T> set) where T : class, IEntity
    {
        public virtual string Exist(Guid id)
        {
            return set.Any(x => x.Id == id) ?
                string.Empty
                : $"Запрашиваемой записи нет";
        }
    }
}
