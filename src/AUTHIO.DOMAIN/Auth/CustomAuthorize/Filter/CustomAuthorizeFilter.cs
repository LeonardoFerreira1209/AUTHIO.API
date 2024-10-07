using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;

namespace AUTHIO.DOMAIN.Auth.CustomAuthorize.Filter;

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
        if (!context.HttpContext.User.Identity.IsAuthenticated)
            throw new UnauthorizedUserException();

        var claimsPrincipal =
            context.HttpContext.User;

        if (HasClaims(claimsPrincipal) is false)
            throw new ForbiddendUserException(null);
    }

    /// <summary>
    /// Verifica se o usuário têm as claims necessárias.
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    private bool HasClaims(ClaimsPrincipal claimsPrincipal)
    {
        var hasClaim = _claims.Any(claim
            => claimsPrincipal.HasClaim(claim.Type, claim.Value)) || _claims.Count == 0;

        return hasClaim;
    }
}
