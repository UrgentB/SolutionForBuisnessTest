namespace SolutionForBuisnessFrontend.Components.Service
{
    public class RequestResult<T>
    {
        public T Data { get; set; }
        public string? Error { get; set; }
    }
}
