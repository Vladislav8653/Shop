﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTO;
using UserManagement.Application.UseCases.Commands.ResetUserCommands.ResetPassword;
using UserManagement.Application.UseCases.Commands.ResetUserCommands.SendResetEmail;

namespace UserManagement.Presentation.Controllers;

[ApiController]
[Route("reset")]
public class ResetUserController(IMediator mediator) : ControllerBase
{
    [Route("send-email")]
    [HttpPost]
    public async Task<IActionResult> SendResetToken
        ([FromBody] EmailForPasswordResetDto emailForPasswordResetDto, CancellationToken cancellationToken)
    {
        var query = new SendResetEmailCommand
        {
            NewPasswordDto = emailForPasswordResetDto
        };
        
        var result = await mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [Route("reset-password")]
    [HttpPost]
    public async Task<IActionResult> ResetPassword
        ([FromBody] ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
    {
        var query = new ResetPasswordCommand
        {
            ResetPasswordDto = resetPasswordDto
        };
       
        var result = await mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}