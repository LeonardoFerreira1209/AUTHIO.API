using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;

namespace AUTHIO.DOMAIN.Auth;

/// <summary>
///  Classe custom de tratamentos de eventos de token bearer.
/// </summary>
public class CustomJwtBearerEvents() : JwtBearerEvents
{
    /// <summary>
    /// Autenticação do token falhou.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedUserException"></exception>
    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        Log.Error($"[LOG ERROR] {nameof(JwtBearerEvents)} - METHOD OnAuthenticationFailed - {context.Exception.Message}\n");

        throw new UnauthorizedUserException(
            "Token do usuário não é permitido ou está incorreto!");
    }

    /// <summary>
    /// Token validado.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task TokenValidated(TokenValidatedContext context)
    {
        Log.Information($"[LOG INFORMATION] {nameof(JwtBearerEvents)} - OnTokenValidated - {context.SecurityToken}\n");

        return Task.CompletedTask;
    }
}
