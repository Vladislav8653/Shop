using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
using ProductManagement.Domain.Models;

namespace ProductManagement.Application.Contracts.RepositoryContracts;

public interface IProductRepository
{
    Task<PagedResult<Product>> GetByParamsAsync(PageParams pageParams, ProductFilters filters,
        CancellationToken cancellationToken);
}