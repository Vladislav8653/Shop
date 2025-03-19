using System.Linq.Expressions;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
using ProductManagement.Domain.Models;

namespace ProductManagement.Application.Contracts.RepositoryContracts;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<PagedResult<Product>> GetByParamsAsync(PageParams pageParams, ProductFilters filters,
        Expression<Func<Product, bool>> expression, CancellationToken cancellationToken);
}