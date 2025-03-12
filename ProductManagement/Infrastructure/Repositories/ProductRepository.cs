using System.Linq.Expressions;
using LinqKit;
using ProductManagement.Application.Contracts.RepositoryContracts;
using ProductManagement.Application.Filtration;
using ProductManagement.Application.Pagination;
using ProductManagement.Domain.Models;

namespace ProductManagement.Infrastructure.Repositories;

public class ProductRepository(ApplicationContext context) : RepositoryBase<Product>(context), IProductRepository
{
    private readonly ApplicationContext _context = context;

    public async Task<PagedResult<Product>> GetByParamsAsync(PageParams pageParams, ProductFilters? filters, 
        CancellationToken cancellationToken)
    {
        IQueryable<Product> query = _context.Products;
        if (filters is null)
        {
            return await GetByPageAsync(query, pageParams, cancellationToken); 
        }
        Expression<Func<Product, bool>> filter = p => true;
        if (filters.ProductName is not null)
            filter = filter.And(p => p.ProductName.Contains(filters.ProductName));
        if (filters.Available.HasValue && filters.Available.Value)
            filter.And(product => product.Available == filters.Available.Value);
        if (filters.MinPrice is not null)
            filter.And(product => product.Price >= filters.MinPrice);
        if (filters.MaxPrice is not null)
            filter.And(product => product.Price <= filters.MaxPrice);
        return await GetByPageAsync(query.Where(filter), pageParams, cancellationToken);
    }
}