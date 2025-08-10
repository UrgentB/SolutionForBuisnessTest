using SolutionForBuisnessTest.Models;

namespace SolutionForBuisnessTest.Controllers.Commands
{
    public class ResourceIncomePostCommand
    {
        public Guid Document { get; set; }
        public Guid Resource { get; set; }
        public Guid Unit { get; set; }
        public uint Income { get; set; }
    }
}
