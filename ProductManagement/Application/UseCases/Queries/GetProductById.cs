using AutoMapper;
using ProductManagement.Application.DTO;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Queries;

public class GetProductById(ProductRepository productRepository, IMapper mapper) : IGetProductById
{
    public async Task<ProductResponseDto> Handle(Guid productId, CancellationToken cancellationToken)
    {
        var products = await productRepository.FindByCondition(product => product.Id == productId,
            false, cancellationToken);
        var product = products.First();
        var productResponseDto = mapper.Map<ProductResponseDto>(product);
        return productResponseDto;
    }
}