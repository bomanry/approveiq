namespace BLH.ApproveIQ.Application.DTOs;

public class B2CUserDto
{
    public Guid? Id { get; set; }
    public string? DisplayName { get; set; }
    public string? UserPrincipalName { get; set; }
    public string? Role { get; set; }
    public bool? AccountEnabled { get; set; }
}
