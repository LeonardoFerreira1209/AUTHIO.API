using AUTHIO.APPLICATION.Domain.Entities;
using System.Linq.Expressions;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;

/// <summary>
/// Repositório genérico.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IGenericRepository<T> where T : class, IEntityBase
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
    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
}
