using Common.Model;
using SuggestionAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace SuggestionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class SuggestionController(ISuggestionService suggestionService, ILogger<SuggestionController> logger) : ControllerBase
{
    private readonly ILogger<SuggestionController> _logger = logger;
    private readonly ISuggestionService _suggestionService = suggestionService;

    [HttpGet("{id:int}")]
    //[ResponseCache(Duration = 60 * 60 * 24, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<ActionResult<Suggestion>> Get(int id)
    {
        try
        {
            return Ok(await _suggestionService.ReadSuggestionAsync(id));
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed reading a suggestion through the controller");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    //[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<ActionResult<List<Suggestion>>> Get()
    {
        try
        {
            return Ok(await Task.Run(_suggestionService.GetAllSuggestionsFromDBAsync));
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed reading a suggestion list through the controller");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Suggestion suggestion)
    {
        try
        {
            return Ok(await _suggestionService.InsertSuggestionAsync(suggestion));
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed insering a suggestion through the controller");
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Suggestion suggestion)
    {
        try
        {
            return Ok(await _suggestionService.UpdateSuggestionAsync(suggestion));
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed updating a suggestion through the controller");
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return Ok(await _suggestionService.DeleteSuggestionAsync(id));
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed deleting a suggestion through the controller");
            return BadRequest(ex.Message);
        }
    }
}
