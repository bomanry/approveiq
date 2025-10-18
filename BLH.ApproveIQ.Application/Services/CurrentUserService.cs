using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using BLH.ApproveIQ.Domain.Services;

namespace BLH.ApproveIQ.Application.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
        }
    }

    public bool UserExists => UserId.HasValue;

    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    public string? FirstName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("FirstName");

    public string? LastName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("LastName");
}
