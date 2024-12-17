using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Contracts.Services.Infrastructure;

/// <summary>
/// Interface de User Store customizada.
/// </summary>
/// <typeparam name="TUser"></typeparam>
public interface ICustomUserStore<TUser>
    : IUserStore<TUser> where TUser : class
{
    /// <summary>
    /// Busca por nome usando expressões lambdas.
    /// </summary>
    /// <param name="normalizedUserName"></param>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TUser> FindByNameWithExpressionAsync(
        string normalizedUserName, Expression<Func<TUser,
            bool>> expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// Busca por id usando expressões lambdas.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TUser> FindByIdWithExpressionAsync(
        Guid id, Expression<Func<TUser,
            bool>> expression, CancellationToken cancellationToken = default);
}
