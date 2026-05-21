using Finanzas.Application.Dtos;
using Finanzas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(IAccountAppService service) : ControllerBase
{
    private readonly IAccountAppService _service = service;

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponse>> GetByName(Guid id)
    {
        try
        {
            var response = await _service.GetAccountByIdAsync(id);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
    }
}
