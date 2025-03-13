namespace ProductManagement.Application.Contracts.UseCasesContracts;

public interface IDeleteProduct
{
    Task Handle(Guid productId, CancellationToken cancellationToken);
}