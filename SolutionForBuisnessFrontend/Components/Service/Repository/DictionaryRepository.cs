using SolutionForBuisnessFrontend.Components.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using SolutionForBuisnessFrontend.Components.Models.Commands;

namespace SolutionForBuisnessFrontend.Components.Service.Repository
{
    public class DictionaryRepository<TEntity>(IHttpClientFactory httpFactory): BaseRepository<TEntity, string, DictionaryPatchCommand>(httpFactory)  where TEntity : DictionaryEntry
    {

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"/api/{typeof(TEntity).Name}/{id}");
            var result = await response.Content.ReadFromJsonAsync<RequestResult<bool>>();
            if (result.Error != null)
                throw new BadHttpRequestException(result.Error);
            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException(response.ReasonPhrase);
            return result.Data;
        }

        public override async Task<Guid> PostAsync(string command)
        {
            //string name = command;
            var response = await _http.PostAsJsonAsync($"/api/{typeof(TEntity).Name}/", command);
            var result = await response.Content.ReadFromJsonAsync<RequestResult<Guid>>();
            if (result.Error != null)
                throw new BadHttpRequestException(result.Error);
            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException(response.ReasonPhrase);
            return result.Data;
        }

        public override async Task<bool> PatchAsync(DictionaryPatchCommand command)
        {
            var response = await _http.PatchAsJsonAsync($"/api/{typeof(TEntity).Name}/", command);
            var result = await response.Content.ReadFromJsonAsync<RequestResult<bool>>();
            if (result.Error != null)
                throw new BadHttpRequestException(result.Error);
            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException(response.ReasonPhrase);
            return result.Data;
        }
    }
}
