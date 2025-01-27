﻿using AUTHIO.DOMAIN.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Interface de serviço de usuários.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Método de registro de usuário.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> RegisterAsync(
        RegisterUserRequest registerUserRequest,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Método de atualização de usuário.
    /// </summary>
    /// <param name="updateUserRequest"></param>
    /// <param name="tenantKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> UpdateAsync(
        UpdateUserRequest updateUserRequest,
        string tenantKey,
        CancellationToken cancellationToken
        );

    /// <summary>
    /// Método de registro 
    /// </summary>
    /// <param name="idWithXTenantKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> GetUserByIdAsync(
        IdWithXTenantKey idWithXTenantKey,
        CancellationToken cancellationToken
    );
}
