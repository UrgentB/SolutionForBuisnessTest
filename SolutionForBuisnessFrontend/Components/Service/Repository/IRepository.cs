namespace SolutionForBuisnessFrontend.Components.Service.Repository
{
    public interface IRepository<TEntity, TAddCommand, TPatchCommand>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<bool> DeleteAsync(Guid id);
        public Task<Guid> PostAsync(TAddCommand command);
        public Task<bool> PatchAsync(TPatchCommand command);
    }
}
