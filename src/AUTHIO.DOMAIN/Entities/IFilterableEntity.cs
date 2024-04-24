using AUTHIO.DOMAIN.Contracts;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Interface de filtragem de entidade.
/// </summary>
public interface IFilterableEntity<TEntity> where TEntity : class
{
    /// <summary>
    /// Configuração de filtragem de entidade.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    Expression<Func<TEntity, bool>> GetFilterExpression(IAuthioContext authioContext);
}
