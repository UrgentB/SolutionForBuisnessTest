using Microsoft.AspNetCore.Mvc;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services;
using SolutionForBuisnessTest.Services.DbContext;
using SolutionForBuisnessTest.Services.Validation;

namespace SolutionForBuisnessTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController(IDbContext dbContext) : DictionaryEntryController<Resource>(dbContext)
    {
        [HttpDelete("{id}")]
        public override IActionResult Delete([FromRoute] Guid id)
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

                error = _validation.ResourceUsedByResourceIncome(id, dbContext.ResourceIncomes);
                var entry = dbContext.Resources.Find(id)!;
                if (error != string.Empty)
                {
                    entry.Active = false;
                    dbContext.Save();
                    result.Error = error;
                    return BadRequest(result);
                }

                dbContext.DictionaryEntry.Remove(entry);
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
