using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services;
using SolutionForBuisnessTest.Services.DbContext;
using SolutionForBuisnessTest.Services.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace SolutionForBuisnessTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController(IDbContext dbContext) : DictionaryEntryController<Unit>(dbContext)
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

                error = _validation.UnitUsedByResourceIncome(id, dbContext.ResourceIncomes);
                var entry = dbContext.Units.Find(id)!;
                if (error != string.Empty)
                {
                    entry.Active = false;
                    dbContext.Save();
                    result.Error = error;
                    return BadRequest(result);
                }

                dbContext.Units.Remove(entry);
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
