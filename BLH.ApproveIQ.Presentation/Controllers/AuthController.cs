using BLH.ApproveIQ.Application.Auth.Commands;
using BLH.ApproveIQ.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BLH.ApproveIQ.Presentation.Controllers;

[Route("api/[controller]")]
public sealed class AuthController : ApiController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email);
        var result = await Sender.Send(command, cancellationToken);

        return HandleResult(result);
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
}