namespace SolutionForBuisnessFrontend.Components.Models
{
    public abstract class DictionaryEntry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
