using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTO;
using UserManagement.Application.UseCases.Commands.TokenCommands.RefreshToken;

namespace UserManagement.Presentation.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController(
    IMediator mediator) : Controller
{
    [HttpPost("refresh")]
    [Authorize]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var command = new RefreshTokenCommand
        {
            TokenDto = tokenDto
        };
        var accessToken = await mediator.Send(command);
        
        return Ok(accessToken);
    }
}
