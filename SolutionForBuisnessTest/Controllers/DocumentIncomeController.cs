using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using SolutionForBuisnessTest.Controllers.Commands;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services;
using SolutionForBuisnessTest.Services.DbContext;
using SolutionForBuisnessTest.Services.Validation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SolutionForBuisnessTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentIncomeController(IDbContext dbDbContext) : ControllerBase
    {
        private ValidationEntityWithIdentificationString<DocumentIncome> _validationEntityWithIdentificationString = new(dbDbContext.DocumentIncomes);

        [HttpPost]
        public IActionResult Create(string number, DateTime date)
        {
            var result = new RequestResult<Guid>();
            try
            {
                var error = _validationEntityWithIdentificationString.AlreadyExists(number);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }
                var document = new DocumentIncome()
                {
                    IdentificationString = number,
                    Date = date
                };
                dbDbContext.DocumentIncomes.Add(document);
                dbDbContext.Save();
                result.Data = document.Id;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpGet("/id")]
        public IActionResult Get(Guid id)
        {
            var result = new RequestResult<DocumentIncome>();
            try
            {
                var error = _validationEntityWithIdentificationString.Exist(id);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                var entry = dbDbContext.DocumentIncomes.Find(id)!;
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
            var result = new RequestResult<IEnumerable<DocumentIncome>>();
            try
            {
                result.Data = dbDbContext.DocumentIncomes.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpPatch]
        public IActionResult Patch(Guid id, [FromBody] DocumentIncomePatchCommand command)
        {
            var result = new RequestResult<bool>();
            try
            {
                var error = _validationEntityWithIdentificationString.Exist(id);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                var entry = dbDbContext.DocumentIncomes.Find(id)!;

                if(!string.IsNullOrEmpty(command.Number))
                    entry.IdentificationString = command.Number;
                if(command.Date.HasValue)
                    entry.Date = (DateTime)command.Date;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            var result = new RequestResult<bool>();
            try
            {
                var error = _validationEntityWithIdentificationString.Exist(id);
                if (error != string.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                var entry = dbDbContext.DocumentIncomes.Find(id);
                dbDbContext.DocumentIncomes.Remove(entry);
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
