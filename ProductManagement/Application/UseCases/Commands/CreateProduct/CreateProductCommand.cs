using MediatR;
using ProductManagement.Application.DTO;

namespace ProductManagement.Application.UseCases.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Unit>
{
    public ProductRequestDto? NewProduct { get; init; }
}