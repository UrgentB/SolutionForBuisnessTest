using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SolutionForBuisnessTest.Models
{
    [Table("DOCUMENT_INCOME")]
    public class DocumentIncome: IEntityWithIdentificationString
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }

        [JsonPropertyName("Number")]
        [Column("NUMBER")]
        public string IdentificationString { get; set; }
        [Column("DATE")]
        public DateTime Date { get; set; }
    }
}
