using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Contracts.Repositories.Base;

/// <summary>
/// Repositório genérico.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericRepository<T> where T : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// Criar.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> CreateAsync(T entity);

    /// <summary>
    /// Atualizar.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    T Update(T entity);

    /// <summary>
    /// Recuperar por id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> GetByIdAsync(Guid id);

    /// <summary>
    /// Recuperar por uma regra custom.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Recuperar todos.
    /// </summary>
    /// <returns></returns>
    public Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

    /// <summary>
    /// Recupera todos paginado.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    Task<PaginatedResponse<T>> GetAllAsyncPaginated(
         int pageNumber, int pageSize, Expression<Func<T, bool>> predicate = null);
}
