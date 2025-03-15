using MediatR;
using ProductManagement.Application.DTO;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;

namespace ProductManagement.Application.UseCases.Queries.GetProducts;

public record GetProductsCommand : IRequest<PagedResult<ProductResponseDto>>
{
    public PageParams PageParams { get; init; } = null!;
    public ProductFilters? Filters { get; init; }
}