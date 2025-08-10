using Microsoft.AspNetCore.Mvc;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services.DbContext;

namespace SolutionForBuisnessTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController(IDbContext dbContext) : DictionaryEntryController<Unit>(dbContext) { }
}
