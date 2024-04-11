using AUTHIO.APPLICATION.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AUTHIO.APPLICATION.Application.Services.Custom;

/// <summary>
/// 
/// </summary>
/// <param name="userManager"></param>
/// <param name="contextAccessor"></param>
/// <param name="claimsFactory"></param>
/// <param name="optionsAccessor"></param>
/// <param name="logger"></param>
/// <param name="schemes"></param>
/// <param name="confirmation"></param>
public class CustomSignInManager(CustomUserManager<UserEntity> userManager, 
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<UserEntity> claimsFactory, 
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<SignInManager<UserEntity>> logger, 
    IAuthenticationSchemeProvider schemes, 
    IUserConfirmation<UserEntity> confirmation)
        : SignInManager<UserEntity>(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
{
    
}
