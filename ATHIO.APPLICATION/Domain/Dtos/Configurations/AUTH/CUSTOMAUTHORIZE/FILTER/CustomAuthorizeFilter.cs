using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.DOMAIN.DTOs.CONFIGURATIONS.AUTH.CUSTOMAUTHORIZE.FILTER;

/// <summary>
/// Classe custom de autenticação de usuários.
/// </summary>
/// <remarks>
/// Filtro de autorização customizavel.
/// </remarks>
/// <param name="claims"></param>
public class CustomAuthorizeFilter(
    List<Claim> claims) 
        : IAuthorizationFilter
{
    private readonly List<Claim> _claims = claims;

    /// <summary>
    /// Autorização customizavel.
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isAuthenticated =
            context.HttpContext.User.Identity.IsAuthenticated;

        var claimsPrincipal =
            context.HttpContext.User;

        if ((isAuthenticated && HasClaims(claimsPrincipal)) is false) throw new UnauthorizedUserException(null);
    }

    /// <summary>
    /// Verifica se o usuário têm as claims necessárias.
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    private bool HasClaims(ClaimsPrincipal claimsPrincipal)
    {
        var hasClaim = _claims.Any(claim
            => claimsPrincipal.HasClaim(claim.Type, claim.Value));

        return hasClaim;
    }
}
