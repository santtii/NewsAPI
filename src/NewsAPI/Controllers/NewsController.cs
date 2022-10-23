﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAPI.Core.Entities;
using NewsAPI.Core.Extensions.FilterAttributes;
using NewsAPI.Core.Helpers;
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
    public IActionResult Get(int? page)
    {
        var users = _newsService.GetAll();
        var paginatedUsers = new PaginatedList<NewsEntity>(users, page ?? 0, 5);
        return Ok(paginatedUsers);
    }


    [HttpPost(), ValidateModel]
    public async Task<IActionResult> Add([FromBody] NewsModel model)
    {
        var response = await _newsService.AddAsync(model);
        return response.GetResult();
    }

    [HttpPut(), ValidateModel]
    public async Task<IActionResult> Update([FromBody] NewsModel model)
    {
        var response = await _newsService.UpdateAsync(model);
        return response.GetResult();
    }

    [HttpDelete(), ValidateModel]
    public async Task<IActionResult> Delete([FromBody] int id)
    {
        var response = await _newsService.DeleteAsync(id);
        return response.GetResult();
    }
}