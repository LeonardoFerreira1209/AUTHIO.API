using AUTHIO.DATABASE.Context;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AUTHIO.INFRASTRUCTURE.Services.Identity;

/// <summary>
/// Classe customizada de UserStore.
/// </summary>
/// <typeparam name="TUser"></typeparam>
/// <param name="context"></param>
/// <param name="describer"></param>
public class CustomUserStore<TUser>(
        AuthIoContext context, 
        IdentityErrorDescriber describer = null) 
    : UserStore<TUser, RoleEntity, AuthIoContext, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, 
        IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>(context, describer), ICustomUserStore<TUser>
    where TUser : IdentityUser<Guid> 
{
    /// <summary>
    /// Busca usuário pelo nome e expressao where.
    /// </summary>
    /// <param name="normalizedUserName"></param>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TUser> FindByNameWithExpressionAsync(string normalizedUserName, 
        Expression<Func<TUser, bool>> expression, CancellationToken cancellationToken = default)
    {
        cancellationToken
           .ThrowIfCancellationRequested();

        ThrowIfDisposed();

        return Users.Where(expression)
                    .FirstOrDefaultAsync((TUser user)
                        => user.NormalizedUserName == normalizedUserName, cancellationToken);
    }
}
