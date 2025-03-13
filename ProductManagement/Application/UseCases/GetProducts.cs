using AutoMapper;
using ProductManagement.Application.Contracts.UseCasesContracts;
using ProductManagement.Application.DTO;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases;

public class GetProducts(ProductRepository productRepository, IMapper mapper) : IGetProducts
{
    public async Task<PagedResult<ProductResponseDto>> Handle(PageParams pageParams, ProductFilters filters, CancellationToken cancellationToken)
    {
        //проверить фильтры и пайдж парамс
        var products = await productRepository.GetByParamsAsync(pageParams, filters, cancellationToken);
        var productsResponseDto = mapper.Map<IEnumerable<ProductResponseDto>>(products.Items);
        return new PagedResult<ProductResponseDto>(productsResponseDto, products.Total);
    }
}