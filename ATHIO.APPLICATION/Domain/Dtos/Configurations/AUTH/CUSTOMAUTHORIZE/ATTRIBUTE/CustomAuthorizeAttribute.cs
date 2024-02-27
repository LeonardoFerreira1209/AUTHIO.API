using AUTHIO.APPLICATION.DOMAIN.DTOs.CONFIGURATIONS.AUTH.CUSTOMAUTHORIZE.FILTER;
using AUTHIO.APPLICATION.DOMAIN.ENUMS;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AUTHIO.APPLICATION.DOMAIN.DTOs.CONFIGURATIONS.AUTH.CUSTOMAUTHORIZE.ATTRIBUTE;

public class CustomAuthorizeAttribute : TypeFilterAttribute
{
    /// <summary>
    /// Atributo de autorização customizavel.
    /// </summary>
    /// <param name="claim"></param>
    /// <param name="values"></param>
    public CustomAuthorizeAttribute(Claims claim, params string[] values) : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] {
            values.Select(value => new Claim(claim.ToString(), value)).ToList()
        };
    }
}
