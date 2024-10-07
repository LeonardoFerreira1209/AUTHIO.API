using AUTHIO.DOMAIN.Auth.CustomAuthorize.Filter;
using AUTHIO.DOMAIN.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AUTHIO.DOMAIN.Auth.CustomAuthorize.Attribute;

/// <summary>
/// Classe de autorização customizada.
/// </summary>
public class AuthorizeAttribute : TypeFilterAttribute
{
    /// <summary>
    /// Atributo de autorização customizavel.
    /// </summary>
    /// <param name="claim"></param>
    /// <param name="values"></param>
    public AuthorizeAttribute(
        Claims claim = Claims.Tenants,
        params string[] values) : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = [

            values?.Select(value => new Claim(claim.ToString(), value)).ToList() 
        ];
    }
}
