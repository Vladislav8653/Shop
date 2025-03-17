using MediatR;
using ProductManagement.Application.DTO;

namespace ProductManagement.Application.UseCases.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<Unit>
{
    public string? UserId {get; init; } 
    public Guid ProductId { get; init; }
    public ProductRequestDto? NewProduct { get; init; }
}