using MediatR;
using ProductManagement.Application.DTO;

namespace ProductManagement.Application.UseCases.Queries.GetProductById;

public record GetProductByIdCommand : IRequest<ProductResponseDto>
{
    public Guid ProductId { get; init; }
}