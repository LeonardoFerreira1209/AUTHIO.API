using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TUser"></typeparam>
public interface ICustomUserStore<TUser> 
    : IUserStore<TUser> where TUser : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="normalizedUserName"></param>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TUser> FindByNameWithExpressionAsync(
        string normalizedUserName, Expression<Func<TUser,
            bool>> expression, CancellationToken cancellationToken = default);
}
