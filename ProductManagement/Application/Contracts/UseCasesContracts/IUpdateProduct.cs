using ProductManagement.Application.DTO;

namespace ProductManagement.Application.Contracts.UseCasesContracts;

public interface IUpdateProduct
{
    Task Handle(Guid productId, ProductRequestDto productRequestDto, CancellationToken cancellationToken);
}