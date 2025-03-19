using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using ProductManagement.Application.Contracts.RepositoryContracts;
using ProductManagement.Application.DTO;
using ProductManagement.Application.Pagination;
using ProductManagement.Domain.Models;

namespace ProductManagement.Application.UseCases.Queries.GetProducts;

public class GetProductsCommandHandler(
    IProductRepository productRepository, 
    IMapper mapper) :
    IRequestHandler<GetProductsCommand, PagedResult<ProductResponseDto>>
{
    public async Task<PagedResult<ProductResponseDto>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        Expression<Func<Product, bool>> expression = p => p.IsActive == true;
        var products = await productRepository.GetByParamsAsync(
            request.PageParams, request.Filters, expression, cancellationToken);
        var productsResponseDto = mapper.Map<IEnumerable<ProductResponseDto>>(products.Items);
        return new PagedResult<ProductResponseDto>(productsResponseDto, products.Total);
    }
}