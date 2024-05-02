using AUTHIO.DOMAIN.Entities;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Helpers.Expressions.Filters;

/// <summary>
/// Classe de expressões de filtragem de usuários.
/// </summary>
public static class UserFilters<TUser> where TUser : UserEntity, new()
{
    /// <summary>
    /// Filtra um usuários pertencente a um tenant.
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterTenantUsers(string tenantKey)
        => entidade =>
            entidade.Tenant != null
                && entidade.Tenant.TenantConfiguration != null
                && entidade.Tenant.TenantConfiguration.TenantKey == tenantKey
                && !entidade.System;

    /// <summary>
    /// Filta usuários que são do sistema.
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterSystemUsers(string tenantKey)
        => entidade =>
            tenantKey == null
                && entidade.TenantId == null
                && entidade.System;

    /// <summary>
    /// Se tiver um tenantkey filtra usuários de tenant se não do sistema.
    /// </summary>
    /// <param name="tenantKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterSystemOrTenantUsers(string tenantKey)
        => CustomLambdaExpressions.Or(
            FilterTenantUsers(tenantKey), FilterSystemUsers(tenantKey));
}
