using AUTHIO.DOMAIN.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Contracts.Services;

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

    /// <summary>
    /// Método responsável por fazer a autenticação do usuário
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<ObjectResult> AuthenticationAsync(LoginRequest loginRequest);
}
