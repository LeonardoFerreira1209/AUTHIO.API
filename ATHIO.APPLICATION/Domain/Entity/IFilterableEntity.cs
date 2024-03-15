using AUTHIO.APPLICATION.Infra.Context;
using System.Linq.Expressions;

namespace AUTHIO.APPLICATION.Domain.Entity;

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
    Expression<Func<TEntity, bool>> GetFilterExpression(AuthIoContext authIoContext);
}
