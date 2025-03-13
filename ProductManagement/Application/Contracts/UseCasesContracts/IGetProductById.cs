using ProductManagement.Application.DTO;

namespace ProductManagement.Application.Contracts.UseCasesContracts;

public interface IGetProductById
{
    Task<ProductResponseDto> Handle(Guid productId, CancellationToken cancellationToken);
}