﻿using AUTHIO.DOMAIN.Entities;
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
    /// <param name="ClientKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterClientUsers(string ClientKey)
        => entidade =>
            entidade.Client != null
                && entidade.Client.ClientConfiguration != null
                && entidade.Client.ClientConfiguration.ClientKey == ClientKey
                && !entidade.System;

    /// <summary>
    /// Filta usuários que são do sistema.
    /// </summary>
    /// <param name="ClientKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterSystemUsers(string ClientKey)
        => entidade =>
            ClientKey == null
                && entidade.ClientId == null
                && entidade.System;

    /// <summary>
    /// Se tiver um Clientkey filtra usuários de Client se não do sistema.
    /// </summary>
    /// <param name="ClientKey"></param>
    /// <returns></returns>
    public static Expression<Func<TUser, bool>> FilterSystemOrClientUsers(string ClientKey)
        => CustomLambdaExpressions.Or(
            FilterClientUsers(ClientKey), FilterSystemUsers(ClientKey));
}
