using MediatR;

namespace ProductManagement.Application.UseCases.Commands.DeleteProduct;

public record DeleteProductCommand : IRequest<Unit>
{
    public Guid ProductId { get; init; }
}