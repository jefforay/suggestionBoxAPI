using Microsoft.AspNetCore.Mvc;
using Common.Model;

namespace SuggestionAPI.Service
{
    public interface ISuggestionService
    {
        Task<bool> DeleteSuggestionAsync(int id);
        Task<List<Suggestion>> GetAllSuggestionsFromDBAsync();
        Task<bool> InsertSuggestionAsync([FromBody] Suggestion suggestion);
        Task<Suggestion> ReadSuggestionAsync(int id);
        Task<bool> UpdateSuggestionAsync([FromBody] Suggestion suggestion);
    }
}