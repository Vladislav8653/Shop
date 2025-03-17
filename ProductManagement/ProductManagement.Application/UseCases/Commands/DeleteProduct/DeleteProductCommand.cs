using MediatR;

namespace ProductManagement.Application.UseCases.Commands.DeleteProduct;

public record DeleteProductCommand : IRequest<Unit>
{
    public string? UserId {get; init; } 
    public Guid ProductId { get; init; }
}