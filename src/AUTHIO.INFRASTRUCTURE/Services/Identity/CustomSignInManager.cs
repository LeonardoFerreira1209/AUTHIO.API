using AUTHIO.DOMAIN.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AUTHIO.INFRASTRUCTURE.Services.Identity;

/// <summary>
/// Classe customizada de SigninManager.
/// </summary>
/// <typeparam name="TUser"></typeparam>
/// <param name="userManager"></param>
/// <param name="contextAccessor"></param>
/// <param name="claimsFactory"></param>
/// <param name="optionsAccessor"></param>
/// <param name="logger"></param>
/// <param name="schemes"></param>
/// <param name="confirmation"></param>
public class CustomSignInManager<TUser>(CustomUserManager<TUser> userManager,
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<TUser> claimsFactory,
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<SignInManager<TUser>> logger,
    IAuthenticationSchemeProvider schemes,
    IUserConfirmation<TUser> confirmation)
        : SignInManager<TUser>(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation) 
            where TUser : UserEntity, new()
{

}
