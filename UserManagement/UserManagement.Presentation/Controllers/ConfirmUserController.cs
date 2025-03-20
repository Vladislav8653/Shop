﻿using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTO;
using UserManagement.Application.UseCases.Commands.ConfirmUserCommands.ConfirmEmail;
using UserManagement.Application.UseCases.Commands.ConfirmUserCommands.SendConfirmation;

namespace UserManagement.Presentation.Controllers;

[Route("api/confirm")]
[ApiController]
public class ConfirmUserController(IMediator mediator): ControllerBase
{
    [Authorize]
    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail
        ([FromBody] ConfirmEmailDto confirmEmailDto, CancellationToken cancellationToken)
    {
        var query = new ConfirmEmailCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            ConfirmationCode = confirmEmailDto.ConfirmationCode
        };
        
        var result = await mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost("send-confirmation")]
    public async Task<IActionResult> SendConfirmationEmail(CancellationToken cancellationToken) 
    {
        var query = new SendConfirmationCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };
        
        var result = await mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}