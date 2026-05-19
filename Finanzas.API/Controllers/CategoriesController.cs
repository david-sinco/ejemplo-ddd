using Finanzas.Application.Dtos;
using Finanzas.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryAppService appService) : ControllerBase
{
    private readonly ICategoryAppService _appService = appService;

    [HttpGet("{name}")]
    public async Task<ActionResult<CategoryResponse>> GetByName(string name)
    {
        try
        {
            var response = await _appService.GetCategoryByNameAsync(name);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        try
        {
            await _appService.CreateCategoryAsync(request);
            return CreatedAtAction(nameof(GetByName), new { name = request.Name }, request);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { ex.Message });
        }
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> Update(string name, [FromBody] UpdateCategoryRequest request)
    {
        try
        {
            await _appService.UpdateCategoryAsync(name, request);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}