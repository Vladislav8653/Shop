using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTO;
using UserManagement.Application.DTO.HideUserDto;
using UserManagement.Application.UseCases.Commands.HideUserCommands;
using UserManagement.Application.UseCases.Commands.UserCommands.Authenticate;
using UserManagement.Application.UseCases.Commands.UserCommands.DeleteById;
using UserManagement.Application.UseCases.Commands.UserCommands.Register;
using UserManagement.Application.UseCases.Queries.UserQueries.GetAllUsers;

namespace UserManagement.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("getAllUsers")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var users = await mediator.Send(query);
        
        return Ok(users);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] UserRequestDto userRequestDto)
    {
        var query = new RegisterUserCommand
        {
            UserRequestDto = userRequestDto
        };
        await mediator.Send(query);
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserDto userForLogin)
    {
        var query = new AuthenticateUserCommand
        {
            AuthenticateUserDto = userForLogin
        };
        var (accessToken, refreshToken) = await mediator.Send(query);
         
        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpDelete("deleteUser/{userId}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var query = new DeleteUserCommand
        {
            UserId = userId
        };
        await mediator.Send(query);
        
        return Ok();
    }

    [HttpPost("hide")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> HideUser
        ([FromBody] HideUserDto hideUserDto, CancellationToken cancellationToken)
    {
        var query = new HideUserCommand
        {
            Token = Request.Headers.Authorization.ToString().Substring("Bearer ".Length).Trim(),
            HideUserDto = hideUserDto
        };
        
        await mediator.Send(query, cancellationToken);

        return Ok();
    }
}
