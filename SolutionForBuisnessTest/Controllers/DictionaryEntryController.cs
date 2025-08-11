using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services;
using SolutionForBuisnessTest.Services.DbContext;
using SolutionForBuisnessTest.Services.Validation;

namespace SolutionForBuisnessTest.Controllers
{
    public abstract class DictionaryEntryController<T>(IDbContext dbContext) : ControllerBase where T : DictionaryEntry, new()
    {
        protected ValidationDictionaryEntry<T> _validation = new(dbContext.DictionaryEntry);


        [HttpPost]
        public IActionResult Create(string name)
        {
            var result = new RequestResult<Guid>();
            try
            {
                var error = _validation.AlreadyExists(name);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                var entry = new T() { IdentificationString = name };
                dbContext.DictionaryEntry.Add(entry);
                dbContext.Save();
                result.Data = entry.Id;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpGet("id")]
        public IActionResult Get(Guid id)
        {
            var result = new RequestResult<T>();
            try
            {
                var error = _validation.Exist(id);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                var entry = dbContext.DictionaryEntry.OfType<T>().First(x => x.Id == id)!;
                result.Data = entry;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = new RequestResult<IEnumerable<T>>();
            try
            {
                result.Data = dbContext.DictionaryEntry.OfType<T>().ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpDelete]
        public virtual IActionResult Delete(Guid id)
        {
            var result = new RequestResult<bool>();
            try
            {
                var error = _validation.Exist(id);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }
                var entry = dbContext.DictionaryEntry.OfType<T>().First(x => x.Id == id)!;
                if (entry.Active)
                {
                    entry.Active = false;
                    dbContext.Save();
                    return Ok(result);
                }

                //так и не придумал как втисунть проверку на связанные записи в валидацию без сильных изменений
                //но я понимаю что это нарушение Single Responsibility principle 
                dbContext.DictionaryEntry.Remove(entry);
                dbContext.Save();
                return Ok(result);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                result.Error = $"{typeof(T).Name} нельзя удалить - есть связанные записи";
                return BadRequest(result);
            } 
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpPatch]
        public IActionResult ChangeName(Guid id, string newName)
        {
            var result = new RequestResult<bool>();
            try
            {
                string error = _validation.Exist(id);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                error = _validation.AlreadyExists(newName);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                var entry = dbContext.DictionaryEntry.OfType<T>().First(x => x.Id == id)!;
                entry.IdentificationString = newName;
                dbContext.Save();
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }
    }
}
