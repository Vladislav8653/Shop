using AutoMapper;
using MediatR;
using ProductManagement.Application.DTO;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Queries.GetProducts;

public class GetProductsCommandHandler(
    ProductRepository productRepository, 
    IMapper mapper) :
    IRequestHandler<GetProductsCommand, PagedResult<ProductResponseDto>>
{
    public async Task<PagedResult<ProductResponseDto>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        //проверить фильтры и пайдж парамс
        var products = await productRepository.GetByParamsAsync(
            request.PageParams, request.Filters, cancellationToken);
        var productsResponseDto = mapper.Map<IEnumerable<ProductResponseDto>>(products.Items);
        return new PagedResult<ProductResponseDto>(productsResponseDto, products.Total);
    }
}