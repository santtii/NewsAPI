using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Core.Extensions.FilterAttributes;
using NewsAPI.Core.Interfaces;
using NewsAPI.Core.Models;

namespace NewsAPI.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet(), ValidateModel]
    public async Task<IActionResult> GetAsync(int? page)
    {
        return Ok(await _newsService.GetAllAsync(page));
    }

    [HttpPost(), ValidateModel]
    public async Task<IActionResult> AddAsync([FromBody] NewsModel model)
    {
        var response = await _newsService.AddAsync(model);
        return response.GetResult();
    }

    [HttpPut(), ValidateModel]
    public async Task<IActionResult> UpdateAsync([FromBody] NewsModel model)
    {
        var response = await _newsService.UpdateAsync(model);
        return response.GetResult();
    }

    [HttpDelete("{id}"), ValidateModel]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await _newsService.DeleteAsync(id);
        return response.GetResult();
    }
}
