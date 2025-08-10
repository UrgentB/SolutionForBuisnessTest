using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolutionForBuisnessTest.Models
{
    [Table("RESOURCE_INCOME")]
    public class ResourceIncome : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DocumentIncome Document { get; set; }
        public Resource Resource { get; set; }
        public Unit Unit { get; set; }
        public uint Income { get; set; }
    }
}
