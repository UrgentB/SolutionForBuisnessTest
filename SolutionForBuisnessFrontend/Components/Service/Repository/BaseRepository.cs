using static System.Net.WebRequestMethods;

namespace SolutionForBuisnessFrontend.Components.Service.Repository
{
    public abstract class BaseRepository<TEntity, TAddCommand, TPatchCommand>(IHttpClientFactory httpFactory) : IRepository<TEntity, TAddCommand, TPatchCommand>
    {
        protected HttpClient _http => httpFactory.CreateClient("ApiClient");
        
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            string r = await _http.GetStringAsync($"api/{typeof(TEntity).Name}");
            var result = await _http.GetFromJsonAsync<RequestResult<IEnumerable<TEntity>>>($"api/{typeof(TEntity).Name}");
            if (result.Error != null)
                throw new BadHttpRequestException(result.Error);
            return result.Data;
        }

        public virtual Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Guid> PostAsync(TAddCommand command)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> PatchAsync(TPatchCommand command)
        {
            throw new NotImplementedException();
        }

    }
}
