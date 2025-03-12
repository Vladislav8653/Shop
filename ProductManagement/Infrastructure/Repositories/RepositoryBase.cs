using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Contracts.RepositoryContracts;
using ProductManagement.Domain.Pagination;

namespace ProductManagement.Infrastructure.Repositories;

public class RepositoryBase<T>(ApplicationContext context) : IRepositoryBase<T> where T : class
{
    public async Task<IEnumerable<T>> FindAll(bool trackChanges, CancellationToken cancellationToken)
    {
        IQueryable<T> query = context.Set<T>();
        if (!trackChanges)
        {
            query.AsNoTracking();
        }
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges, CancellationToken cancellationToken)
    {
        IQueryable<T> query = context.Set<T>();
        if (!trackChanges)
        {
            query.AsNoTracking();
        }
        query = query.Where(expression);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task Create(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<PagedResult<T>> GetByPageAsync(IQueryable<T> query, PageParams pageParams, CancellationToken cancellationToken)
    {
        var totalCount = query.Count();
        var page = pageParams.Page;
        var pageSize = pageParams.PageSize;
        var skip = (page - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        return new PagedResult<T>(await query.ToListAsync(cancellationToken), totalCount);
    }
}