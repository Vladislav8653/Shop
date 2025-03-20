using MediatR;
using ProductManagement.Application.DTO;

namespace ProductManagement.Application.UseCases.Commands.HideProduct;

public record ToggleProductVisibilityCommand : IRequest<Unit>
{
    public ProductVisibilityRequestDto ProductVisibilityRequestDto { get; init; }
}