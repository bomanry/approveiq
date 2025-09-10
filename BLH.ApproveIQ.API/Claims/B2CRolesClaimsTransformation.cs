using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using BLH.ApproveIQ.Domain.Entities;
using BLH.ApproveIQ.Domain.Identity.Models;
using BLH.ApproveIQ.Persistence;

namespace BLH.ApproveIQ.API.Claims;

public class B2CRolesClaimsTransformation : IClaimsTransformation
{
    private readonly ApplicationDbContext _dbContext;
    
    public B2CRolesClaimsTransformation(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var nameIdentifier = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = _dbContext.Set<B2CUser>()
            .SingleOrDefault(m => m.Id.ToString() == (nameIdentifier ?? ""));

        if (user != null)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            
            var userIdClaimType = ApplicationIdentityConstants.UserIdClaimType;
            claimsIdentity.AddClaim(new Claim(userIdClaimType, user.Id.ToString()));
            
            var assignedRoleClaimType = ApplicationIdentityConstants.AssignedRoleClaimType;
            if (!principal.HasClaim(claim => claim.Type == assignedRoleClaimType))
            {
                claimsIdentity.AddClaim(new Claim(assignedRoleClaimType, user.Role));
                
            }

            principal.AddIdentity(claimsIdentity);
        }
        
        return Task.FromResult(principal);
    }
}