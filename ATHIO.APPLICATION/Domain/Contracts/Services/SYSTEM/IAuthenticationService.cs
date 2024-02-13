using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST.SYSTEM;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.APPLICATION.Domain.Contracts.Services.System;

/// <summary>
/// Interface de serviço de autenticação de usuários.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Método de registro de usuário.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> RegisterAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken);
}
