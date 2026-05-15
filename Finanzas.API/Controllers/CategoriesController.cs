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
            var category = await _appService.GetCategoryByNameAsync(name);
            var response = new CategoryResponse(
                category.Id,
                category.Name.Value,
                category.Color.Value,
                category.Icon);

            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
    {
        try
        {
            await _appService.CreateCategoryAsync(request.Name, request.Color, request.Icon);
            return CreatedAtAction(nameof(GetByName), new { name = request.Name }, request);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { Message = ex.Message });
        }
    }

    [HttpPut("{name}")]
    public async Task<IActionResult> Update(string name, [FromBody] UpdateCategoryRequest request)
    {
        try
        {
            await _appService.UpdateCategoryAsync(name, request.Color, request.Icon);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}