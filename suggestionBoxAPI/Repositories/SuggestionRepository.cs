using SuggestionAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace SuggestionAPI.Repositories;

public sealed class SuggestionRepository(SuggestionDbContext context) : ISuggestionRepository
{
    private readonly DbSet<Suggestion> _suggestionTable = context.Set<Suggestion>();
    private readonly SuggestionDbContext _context = context;

    public async Task<bool> DeleteSuggestionAsync(int id)
    {
        Suggestion? suggestion = await _suggestionTable.FindAsync(id);

        if (suggestion != null)
        {
            await _suggestionTable.Where(l => l.Id == id).ExecuteDeleteAsync();
        }

        return true;
    }

    public async Task<bool> InsertSuggestionAsync(Suggestion suggestion)
    {
        await _suggestionTable.AddAsync(suggestion);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateSuggestionAsync(Suggestion suggestion)
    {
        Suggestion? existingSuggestion = await _suggestionTable.FindAsync(suggestion.Id);

        if (existingSuggestion != null)
        {
            _suggestionTable.Entry(existingSuggestion).CurrentValues.SetValues(suggestion);
            await _context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<Suggestion> ReadSuggestionAsync(int id)
        => await _suggestionTable.FirstOrDefaultAsync(suggestion => suggestion.Id == id) ?? new Suggestion();

    public async Task<List<Suggestion>> GetAllSuggestionsFromDBAsync()
    => await _suggestionTable
            .OrderByDescending(suggestion => suggestion.Id)
            .ToListAsync();
}

