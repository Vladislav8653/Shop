using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Application.UseCases.Commands;

public class DeleteProduct(ProductRepository productRepository) : IDeleteProduct
{
    public async Task Handle(Guid productId, CancellationToken cancellationToken)
    {
        var products = await productRepository.FindByCondition(product => product.Id == productId,
            false, cancellationToken);
        var product = products.First();
        await productRepository.Delete(product, cancellationToken);
    }
}