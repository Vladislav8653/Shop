using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace UserManagement.Application.UseCases.Commands.HideUserCommands;

public class HideUserCommandHandler(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration) :
    IRequestHandler<HideUserCommand, Unit>
{
    public async Task<Unit> Handle(HideUserCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.HideUserDto.UserId, out _))
        {
            throw new ValidationException("UserId is invalid");
        }

        var productMicroServiceConfig = configuration.GetSection("ProductManagement");
        var rootUrl = productMicroServiceConfig["rootUrl"];
        var route = productMicroServiceConfig["HideProductsRoute"];
        if (string.IsNullOrEmpty(rootUrl) || string.IsNullOrEmpty(route))
        {
            throw new InvalidOperationException("ProductMicroServiceConfig is invalid");
        }
        
        var client = httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);
        var url = $"{rootUrl}/{route}";
        var response = await client.PostAsJsonAsync(url, request.HideUserDto, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error: {response.ReasonPhrase}");   
        }
        
        return Unit.Value;
    }
}