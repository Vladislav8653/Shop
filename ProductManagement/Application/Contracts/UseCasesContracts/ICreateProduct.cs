using ProductManagement.Application.DTO;
using ProductManagement.Domain.Models;

namespace ProductManagement.Application.Contracts.UseCasesContracts;

public interface ICreateProduct
{
    Task Handle(ProductRequestDto productRequestDto, CancellationToken cancellationToken);
}