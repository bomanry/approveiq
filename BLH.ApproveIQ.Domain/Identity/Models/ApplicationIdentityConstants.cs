namespace BLH.ApproveIQ.Domain.Identity.Models;

public class ApplicationIdentityConstants
{
    public const string UserIdClaimType = "UserId";
    public const string AssignedTutorIdClaimType = "AssignedTutorId";
    public const string AssignedRoleClaimType = "AssignedRole";

    public enum Roles
    {
        Administrator,
        DistrictAdmin,
        Tutor,
        NoAccess
    }
}
