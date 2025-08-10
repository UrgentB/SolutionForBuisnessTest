using Microsoft.AspNetCore.Mvc;
using SolutionForBuisnessTest.Models;
using SolutionForBuisnessTest.Services.DbContext;

namespace SolutionForBuisnessTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController(IDbContext dbContext) : DictionaryEntryController<Resource>(dbContext)
    {
    }
}
