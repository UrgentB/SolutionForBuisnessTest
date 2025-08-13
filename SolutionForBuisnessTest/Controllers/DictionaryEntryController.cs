using Microsoft.AspNetCore.Mvc;
using SolutionForBuisnessTest.Controllers.Commands;
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
        public virtual IActionResult Create([FromBody] string name)
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
        public virtual IActionResult Get([FromRoute] Guid id)
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
        public virtual IActionResult GetAll()
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
        public abstract IActionResult Delete([FromQuery] Guid id);

        [HttpPatch]
        public virtual IActionResult ChangeName([FromBody] DictionaryPatchCommand command)
        {
            var result = new RequestResult<bool>();
            try
            {
                string error = _validation.Exist(command.Id);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                error = _validation.AlreadyExists(command.Name);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                var entry = dbContext.DictionaryEntry.OfType<T>().First(x => x.Id == command.Id)!;
                entry.IdentificationString = command.Name;
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
