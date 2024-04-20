using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AUTHIO.APPLICATION.Infra.Services.Custom;

/// <summary>
/// Classe customizada de validação de usuários.
/// </summary>
public class CustomUserValidator<TUser>(ITenantIdentityConfigurationRepository tenantIdentityConfigurationRepository,
        IContextService contextService)
    : UserValidator<TUser> where TUser : class
{
    private readonly ITenantIdentityConfigurationRepository
        _tenantIdentityConfigurationRepository = tenantIdentityConfigurationRepository;

    private readonly string _tenantKey
        = contextService.GetCurrentTenantKey();

    /// <summary>
    /// Valida o usuário.
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public override async Task<IdentityResult> ValidateAsync(
        UserManager<TUser> manager, TUser user)
    {
        var tenantIdentityConfigurationEntity = await _tenantIdentityConfigurationRepository
            .GetAsync(config => config.TenantConfiguration.TenantKey == _tenantKey)
                .ContinueWith((tenantIdentityTask) =>
                {
                    var tenantIdentityConfigurationEntity
                        = tenantIdentityTask;

                    return tenantIdentityConfigurationEntity;

                }).Result;

        UserOptions userOptions
            = tenantIdentityConfigurationEntity?
                .UserIdentityConfiguration;

        return await ValidateUserName(manager, user, userOptions).ContinueWith(
           async (taskIdentityError) =>
           {
               var identityErrors
                   = taskIdentityError.Result;

               bool requireUniqueEmail = userOptions is not null
                   ? userOptions.RequireUniqueEmail
                   : manager.Options.User.RequireUniqueEmail;

               if (requireUniqueEmail)
                   identityErrors = await ValidateEmail(
                       manager, user, identityErrors);

               return !(identityErrors?.Count > 0)
                   ? IdentityResult.Success
                   : IdentityResult.Failed([.. identityErrors]);

           }).Result;
    }

    /// <summary>
    /// Valida o username do usuário.
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="user"></param>
    /// <param name="userOptions"></param>
    /// <returns></returns>
    private async Task<List<IdentityError>> ValidateUserName(
        UserManager<TUser> manager, TUser user,
        UserOptions userOptions)
    {
        List<IdentityError> errors = null;

        string allowedUserNameCharacters = userOptions is not null
                  ? userOptions.AllowedUserNameCharacters
                  : manager.Options.User.AllowedUserNameCharacters;

        return await manager.GetUserNameAsync(user).ContinueWith(
            async (taskString) =>
            {
                var userName
                    = taskString.Result;

                if (string.IsNullOrWhiteSpace(userName))
                {

                    errors ??= [];
                    errors.Add(Describer.InvalidUserName(userName));
                }
                else if (!string.IsNullOrEmpty(
                    allowedUserNameCharacters)
                        && userName.Any(
                            c => !allowedUserNameCharacters.Contains(c)))
                {

                    errors ??= [];
                    errors.Add(Describer.InvalidUserName(userName));
                }
                else
                {
                    var owner = await manager.FindByNameAsync(userName);

                    if (owner != null &&
                        !string.Equals(await manager.GetUserIdAsync(
                            owner), await manager.GetUserIdAsync(user)))
                    {

                        errors ??= [];
                        errors.Add(Describer.DuplicateUserName(userName));
                    }
                }

                return errors;

            }).Result;
    }

    /// <summary>
    /// Valida o e-mail do usuário.
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="user"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    private async Task<List<IdentityError>> ValidateEmail(
        UserManager<TUser> manager, TUser user, List<IdentityError> errors)
    {
        return await manager.GetEmailAsync(user).ContinueWith(
            async (taskString) =>
            {
                var email
                    = taskString.Result;

                if (string.IsNullOrWhiteSpace(email))
                {

                    errors ??= [];
                    errors.Add(Describer.InvalidEmail(email));
                    return errors;
                }

                if (!new EmailAddressAttribute().IsValid(email))
                {

                    errors ??= [];
                    errors.Add(Describer.InvalidEmail(email));
                    return errors;
                }

                var owner = await manager.FindByEmailAsync(email);

                if (owner != null &&
                    !string.Equals(await manager.GetUserIdAsync(
                        owner), await manager.GetUserIdAsync(user)))
                {
                    errors ??= [];
                    errors.Add(Describer.DuplicateEmail(email));
                }

                return errors;

            }).Result;
    }
}
