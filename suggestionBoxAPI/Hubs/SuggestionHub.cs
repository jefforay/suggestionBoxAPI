using Common.Model;
using Microsoft.AspNetCore.SignalR;

namespace SuggestionAPI.Hubs;

public sealed class SuggestionHub(ILogger<SuggestionHub> logger) : Hub
{
    private readonly ILogger<SuggestionHub> _logger = logger;

    public async Task ReceiveSuggestion(Suggestion suggestion)
    {
        try
        {
            await Clients.All.SendAsync("receiveSuggestion", suggestion);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed receiving a Suggestion through the suggestionHub");
        }
    }

    public async Task ReceiveListSuggestion(List<Suggestion> suggestionList)
    {
        try
        {
            await Clients.All.SendAsync("receiveListSuggestion", suggestionList);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed receiving a suggestion list through the suggestionHub");
        }
    }

    public async Task EditSuggestion(Suggestion suggestion)
    {
        try
        {
            await Clients.All.SendAsync("editSuggestion", suggestion);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed editing a suggestion through the suggestionHub");
        }
    }

    public async Task RemoveSuggestion(int id)
    {
        try
        {
            await Clients.All.SendAsync("removeSuggestion", id);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed removing a suggestion through the suggestionHub");
        }
    }
}