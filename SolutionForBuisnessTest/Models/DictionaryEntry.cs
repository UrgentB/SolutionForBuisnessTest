using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SolutionForBuisnessTest.Models
{
    public abstract class DictionaryEntry : IEntityWithIdentificationString
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }

        [JsonPropertyName("Name")]
        [Column("NAME")]
        public string IdentificationString { get; set; }
        [Column("ACTIVE")] 
        public bool Active { get; set; } = true;
    }
}
