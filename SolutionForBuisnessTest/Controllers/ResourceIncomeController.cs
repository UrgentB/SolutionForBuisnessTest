using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolutionForBuisnessTest.Controllers.Commands;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services;
using SolutionForBuisnessTest.Services.DbContext;
using SolutionForBuisnessTest.Services.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SolutionForBuisnessTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceIncomeController(IDbContext dbDbContext) : ControllerBase
    {
        private BaseValidation<ResourceIncome> _validationResourceIncome = new(dbDbContext.ResourceIncomes);
        private ValidationDictionaryEntry<Resource> _validationResource = new(dbDbContext.DictionaryEntry);
        private ValidationDictionaryEntry<Unit> _validationUnit = new(dbDbContext.DictionaryEntry);

        private ValidationEntityWithIdentificationString<DocumentIncome> _validationDocumentIncome =
            new(dbDbContext.DocumentIncomes);

        [HttpPost]
        public IActionResult Create([FromBody] ResourceIncomePostCommand command)
        {
            var result = new RequestResult<Guid>();
            try
            {
                string error = string.Empty;
                error = _validationResource.Activity(command.Resource);
                if (error == string.Empty)
                    error = _validationUnit.Activity(command.Unit);
                if (error == string.Empty)
                    error = _validationDocumentIncome.Exist(command.Document);
                result.Error = error;
                if (error != string.Empty)
                    return BadRequest(result);

                var resourceIncome = new ResourceIncome()
                {
                    Document = dbDbContext.DocumentIncomes.Find(command.Document)!,
                    Resource = dbDbContext.Resources.Find(command.Resource)!,
                    Unit = dbDbContext.Units.Find(command.Unit)!,
                    Income = command.Income
                };
                dbDbContext.ResourceIncomes.Add(resourceIncome);
                dbDbContext.Save();
                result.Data = resourceIncome.Id;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }


        /// <summary>
        /// Возвращает перечисление ресурсов поступления для некоторого документа поступления
        /// </summary>
        /// <param name="documentIncomeId">Идентификатор документа для которого возвращается перечисление ресурсов поступления</param>
        /// <returns>Перечисление ресурсов поступления для некоторого документа поступления</returns>
        [HttpGet]
        public IActionResult GetResourceIncomeByDocumentIncome(Guid documentIncomeId)
        {
            var result = new RequestResult<IEnumerable<ResourceIncome>>();
            try
            {
                string error = string.Empty;
                error = _validationDocumentIncome.Exist(documentIncomeId);
                if (error != String.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                result.Data = dbDbContext.ResourceIncomes.Where(r => r.Document.Id == documentIncomeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                return BadRequest(result);
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] ResourceIncomePatchCommand command)
        {
            var result = new RequestResult<bool>();
            try
            {
                string error = string.Empty;

                error = _validationResourceIncome.Exist(command.Id);
                if (error != String.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                var resourceIncome = dbDbContext.ResourceIncomes.Find(command.Id);

                if (command.Resource.HasValue)
                {
                    error = _validationUnit.Activity((command.Resource.Value));
                    if (error != String.Empty)
                    {
                        result.Error = error;
                        return BadRequest(result);
                    }

                    resourceIncome.Resource = dbDbContext.Resources.Find(command.Resource)!;
                }

                if (command.Unit.HasValue)
                {
                    error = _validationResource.Activity((command.Unit.Value));
                    if (error != String.Empty)
                    {
                        result.Error = error;
                        return BadRequest(result);
                    }

                    resourceIncome.Unit = dbDbContext.Units.Find(command.Unit)!;
                }

                if (command.Document.HasValue)
                {
                    error = _validationDocumentIncome.Exist((command.Document.Value));
                    if (error != String.Empty)
                    {
                        result.Error = error;
                        return BadRequest(result);
                    }

                    resourceIncome.Document = dbDbContext.DocumentIncomes.Find(command.Document)!;
                }

                dbDbContext.Save();
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
                string error = string.Empty;
                error = _validationResourceIncome.Exist(id);
                if (error != String.Empty)
                {
                    result.Error = error;
                    return BadRequest(result);
                }

                dbDbContext.ResourceIncomes.Remove(dbDbContext.ResourceIncomes.Find(id)!);
                dbDbContext.Save();
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
