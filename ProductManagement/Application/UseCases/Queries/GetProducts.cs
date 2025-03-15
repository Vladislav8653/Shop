using AutoMapper;
using MediatR;
using ProductManagement.Application.DTO;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
using ProductManagement.Application.Queries;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Queries;

public class GetProducts(ProductRepository productRepository, IMapper mapper) : IGetProducts, 
    IRequestHandler<GetProductsQuery, PagedResult<ProductResponseDto>>
{
    public async Task<PagedResult<ProductResponseDto>> Handle(PageParams pageParams, ProductFilters filters, CancellationToken cancellationToken)
    {
        //проверить фильтры и пайдж парамс
        var products = await productRepository.GetByParamsAsync(pageParams, filters, cancellationToken);
        var productsResponseDto = mapper.Map<IEnumerable<ProductResponseDto>>(products.Items);
        return new PagedResult<ProductResponseDto>(productsResponseDto, products.Total);
    }

    public async Task<PagedResult<ProductResponseDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}