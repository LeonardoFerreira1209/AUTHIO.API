using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AUTHIO.DATABASE.Repositories.BASE;

/// <summary>
/// Repositório genérico com Entity.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="context"></param>
public class GenericEntityCoreRepository<T>(DbContext context)
    : IGenerictEntityCoreRepository<T> where T : class, IEntityPrimaryKey<Guid>
{
    private readonly DbContext _context = context;

    /// <summary>
    /// Criar.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

        return entity;
    }

    /// <summary>
    /// Criar vários.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<IList<T>> BulkInsertAsync(IList<T> entities)
    {
        await _context.BulkInsertAsync(entities);

        return entities;
    }

    /// <summary>
    /// Atualizar.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);

        return await 
            Task.FromResult(entity);
    }

    /// <summary>
    /// Atualizar vários.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task<IList<T>> BulkUpdateAsync(IList<T> entities)
    {
        await _context.BulkUpdateAsync(entities);

        return entities;
    }

    /// <summary>
    /// Deletar.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public virtual async Task BulkDeleteAsync(IList<T> entities)
        => await _context.BulkDeleteAsync(entities);

    /// <summary>
    /// Recuperar por Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="laziLoading"></param>
    /// <returns></returns>
    public virtual Task<T> GetByIdAsync(Guid id)
        => _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id.Equals(id));

    /// <summary>
    /// Recupera um registro do tipo T. Um predicado fornecido para filtrar o registro.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null)
        => await _context.Set<T>().FirstOrDefaultAsync(predicate);

    /// <summary>
    /// Quantidade total de itens na tabela.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (predicate != null) query = query.Where(predicate);

        return await query.CountAsync();
    }

    /// <summary>
    ///  Recupera todos os registros do tipo T. Um predicado opcional pode ser fornecido para filtrar os registros.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<IList<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (predicate != null)
            query = MountDynamicWhere(
                query, predicate);

        return await query.ToListAsync();
    }

    /// <summary>
    /// Recupera todos paginado.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public virtual async Task<PaginatedResponse<T>> GetAllAsyncPaginated(
         int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (predicate != null)
            query = MountDynamicWhere(
                query, predicate);

        int skip = (pageNumber - 1) * pageSize;
        int totalCount = await query.CountAsync();

        var items
            = await query
                .Skip(skip)
                    .Take(pageSize).ToListAsync();

        return new PaginatedResponse<T>(
                items, totalCount, pageNumber, pageSize
            );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    private IQueryable<T> MountDynamicWhere(
        IQueryable<T> query,
            Expression<Func<T, bool>> predicate) => query.Where(predicate);
}
