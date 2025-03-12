using System.Linq.Expressions;
using ProductManagement.Domain.Pagination;

namespace ProductManagement.Application.Contracts.RepositoryContracts;

public interface IRepositoryBase<T>
{
    public Task<IEnumerable<T>> FindAll(bool trackChanges, CancellationToken cancellationToken);
    
    public Task<IEnumerable<T>> FindByCondition(
        Expression<Func<T, bool>> expression,
        bool trackChanges,
        CancellationToken cancellationToken);
    
    Task Create(T entity, CancellationToken cancellationToken);
    
    Task Update(T entity, CancellationToken cancellationToken);
    
    Task Delete(T entity, CancellationToken cancellationToken);

    Task<PagedResult<T>> GetByPageAsync(IQueryable<T> query, PageParams pageParams,
        CancellationToken cancellationToken);
}