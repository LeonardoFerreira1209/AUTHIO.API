using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Classe de request de autenticação de usuários.
/// </summary>
public class AuthenticationRequest
{
    /// <summary>
    /// Nome de usuário.
    /// </summary>
    [FromHeader]
    public string Username { get; set; }

    /// <summary>
    /// Senha
    /// </summary>
    [FromHeader]
    public string Password { get; set; }

    /// <summary>
    /// Chave do tenant.
    /// </summary>
    [FromHeader]
    public string TenantKey { get; set; }
}
