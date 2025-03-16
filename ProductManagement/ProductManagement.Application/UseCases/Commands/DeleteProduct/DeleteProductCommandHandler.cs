using MediatR;
using ProductManagement.Application.Contracts.RepositoryContracts;

namespace ProductManagement.Application.UseCases.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductRepository productRepository) :
    IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var products = await productRepository
            .FindByCondition(product => product.Id == request.ProductId,
            false, cancellationToken);
        var product = products.First();
        
        await productRepository.Delete(product, cancellationToken);
        
        return Unit.Value;
    }
}