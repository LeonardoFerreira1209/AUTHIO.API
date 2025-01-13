using AUTHIO.DOMAIN.Entities;
using System.Linq.Expressions;

namespace AUTHIO.DOMAIN.Helpers.Expressions.Filters;

/// <summary>
/// Classe de expressões de filtragem de usuários.
/// </summary>
public static class UserFilters<TUser> where TUser : UserEntity, new()
{
    /// <summary>
    /// Filtra um usuários pertencente a um Client.
    /// </summary>
    /// <param name="clientKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterClientUsers(string clientKey)
        => entidade =>
            entidade.Client != null
                && entidade.Client.ClientConfiguration != null
                && entidade.Client.ClientConfiguration.ClientKey == clientKey
                && !entidade.System;

    /// <summary>
    /// Filta usuários que são do sistema.
    /// </summary>
    /// <param name="clientKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterSystemUsers(string clientKey)
        => entidade =>
            clientKey == null
                && entidade.ClientId == null
                && entidade.System;

    /// <summary>
    /// Se tiver um Clientkey filtra usuários de Client se não do sistema.
    /// </summary>
    /// <param name="clientKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterSystemOrClientUsers(string clientKey)
        => CustomLambdaExpressions.Or(
            FilterClientUsers(clientKey), FilterSystemUsers(clientKey));
}
