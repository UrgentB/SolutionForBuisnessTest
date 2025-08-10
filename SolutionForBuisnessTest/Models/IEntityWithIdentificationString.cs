namespace SolutionForBuisnessTest.Models
{
    public interface IEntityWithIdentificationString: IEntity
    {
        public Guid Id { get; set; }
        public string IdentificationString { get; set; }
    }
}
