using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTO;
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
        var command = new RegisterUserCommand
        {
            UserRequestDto = userRequestDto
        };
        await mediator.Send(command);
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserDto userForLogin)
    {
        var command = new AuthenticateUserCommand
        {
            AuthenticateUserDto = userForLogin
        };
        var (accessToken, refreshToken) = await mediator.Send(command);
         
        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpDelete("deleteUser/{userId}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var command = new DeleteUserCommand
        {
            UserId = userId
        };
        await mediator.Send(command);
        
        return Ok();
    }
}
