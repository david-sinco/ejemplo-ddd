using System.Security.Claims;
using Finanzas.Application.Interfaces;

namespace Finanzas.API;

public class HttpUserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


    private ClaimsPrincipal? _principal => _httpContextAccessor.HttpContext?.User;
    private string? UserIdString => _principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;


    public Guid? UserId => UserIdString != null ? Guid.Parse(UserIdString) : null;  
    public bool IsAuthenticated => _principal?.Identity?.IsAuthenticated ?? false;
}
