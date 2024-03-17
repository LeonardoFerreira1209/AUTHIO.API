using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AUTHIO.APPLICATION.Infra.Repositories.BASE;

/// <summary>
/// Repositório genérico com Entity.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="context"></param>
public class GenericEntityCoreRepository<T>(DbContext context)
    : IGenerictEntityCoreRepository<T> where T : class, IEntityBase
{
    private readonly DbContext _context = context;

    /// <summary>
    /// Criar.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

        return entity;
    }

    /// <summary>
    /// Criar vários.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<IList<T>> BulkInsertAsync(IList<T> entities)
    {
        await _context.BulkInsertAsync(entities);

        return entities;
    }

    /// <summary>
    /// Atualizar.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);

        return entity;
    }

    /// <summary>
    /// Atualizar vários.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<IList<T>> BulkUpdateAsync(IList<T> entities)
    {
        await _context.BulkUpdateAsync(entities);

        return entities;
    }

    /// <summary>
    /// Deletar.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task BulkDeleteAsync(IList<T> entities)
        => await _context.BulkDeleteAsync(entities);

    /// <summary>
    /// Recuperar por Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="laziLoading"></param>
    /// <returns></returns>
    public Task<T> GetByIdAsync(Guid id)
        => _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id.Equals(id));

    /// <summary>
    /// Recupera um registro do tipo T. Um predicado fornecido para filtrar o registro.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate = null)
        => await _context.Set<T>().FirstOrDefaultAsync(predicate);

    /// <summary>
    /// Recupera todos os registros do tipo T. Um predicado opcional pode ser fornecido para filtrar os registros.
    /// </summary>
    /// <param name="predicate">Um predicado opcional para filtrar os registros recuperados.</param>
    /// <returns>Uma tarefa que representa a operação de recuperação. O valor da tarefa é uma IQueryable<T> contendo todos os registros ou registros filtrados baseados no predicado.</returns>
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (predicate != null) query = query.Where(predicate);

        return await query.ToListAsync();
    }
}
