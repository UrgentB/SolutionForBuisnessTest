using Microsoft.EntityFrameworkCore;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services.DbContext;

namespace SolutionForBuisnessTest.Services.Validation
{
    public class ValidationDictionaryEntry<T>(DbSet<DictionaryEntry> set)
        : ValidationEntityWithIdentificationString<DictionaryEntry>(set) where T : DictionaryEntry

    {
        public override string Exist(Guid id)
        {
            return set.OfType<T>().Any(x => x.Id == id) ?
                string.Empty
                : $"Запрашиваемой записи {typeof(T).Name} нет";
        }

        public override string AlreadyExists(string name)
        {
            return set.OfType<T>().Any(x => x.IdentificationString == name) ?
                $"Запись {typeof(T).Name} с таким именем уже есть"
                : string.Empty;
        }

        public string Activity(Guid id)
        {
            var error = string.Empty;
            error = Exist(id);
            if(error == string.Empty)
                return set.OfType<T>().First(x => x.Id == id)!.Active == false
                    ? $"{typeof(T).Name} нельзя прикрепить, так как неакивен"
                    : string.Empty;
            return error;
        }
    }
}
