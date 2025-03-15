using AutoMapper;
using ProductManagement.Application.DTO;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Commands;

public class UpdateProduct(ProductRepository productRepository, IMapper mapper) : IUpdateProduct
{
    public async Task Handle(Guid productId, ProductRequestDto productRequestDto, CancellationToken cancellationToken)
    {
        var products = await productRepository.FindByCondition(product => product.Id == productId,
            false, cancellationToken);
        var product = products.First();
        mapper.Map(productRequestDto, product);
        await productRepository.Update(product, cancellationToken);
    }
}