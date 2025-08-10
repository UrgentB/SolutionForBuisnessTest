using SolutionForBuisnessTest.Models;

namespace SolutionForBuisnessTest.Controllers.Commands
{
    public class ResourceIncomePatchCommand
    {
        public Guid Id { get; set; }
        public Guid? Document { get; set; }
        public Guid? Resource { get; set; }
        public Guid? Unit { get; set; }
        public uint? Income { get; set; }
    }
}
