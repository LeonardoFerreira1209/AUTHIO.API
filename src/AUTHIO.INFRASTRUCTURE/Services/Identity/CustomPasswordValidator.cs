using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Helpers.Extensions;
using Microsoft.AspNetCore.Identity;

namespace AUTHIO.INFRASTRUCTURE.Services.Identity;

/// <summary>
/// Classe customizada de validação de senha.
/// </summary>
/// <typeparam name="TUser"></typeparam>
/// <param name="tenantIdentityConfigurationRepository"></param>
/// <param name="contextService"></param>
/// <param name="customIdentityErrorDescriber"></param>
public class CustomPasswordValidator<TUser>(ITenantIdentityConfigurationRepository tenantIdentityConfigurationRepository,
                               IContextService contextService, CustomIdentityErrorDescriber customIdentityErrorDescriber = null)
    : PasswordValidator<TUser>(customIdentityErrorDescriber) where TUser : class
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
    /// <param name="password"></param>
    /// <returns></returns>
    public override async Task<IdentityResult> ValidateAsync(
        UserManager<TUser> manager, TUser user, string password)
    {
        var tenantIdentityConfigurationEntity = await _tenantIdentityConfigurationRepository
            .GetAsync(config => config.TenantConfiguration.TenantKey == _tenantKey)
                .ContinueWith((tenantIdentityTask) =>
                {
                    var tenantIdentityConfigurationEntity
                        = tenantIdentityTask;

                    return tenantIdentityConfigurationEntity;

                }).Result;

        PasswordOptions passwordOptions
            = tenantIdentityConfigurationEntity?
                .PasswordIdentityConfiguration;

        return await ValidatePasswordAsync(manager, user, password, passwordOptions).ContinueWith(
           (taskIdentityResult) =>
           {
               var identityResult
                   = taskIdentityResult;

               return identityResult;

           }).Result;
    }

    /// <summary>
    /// Valida a senha do usuário baseado no PasswordOptions.
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="passwordOptions"></param>
    /// <returns></returns>
    private async Task<IdentityResult> ValidatePasswordAsync(UserManager<TUser> manager, TUser user,
        string password, PasswordOptions passwordOptions)
    {
        List<IdentityError> errors = null;
        var options = manager.Options.Password;

        int requiredLength = passwordOptions is not null
                  ? passwordOptions.RequiredLength
                  : manager.Options.Password.RequiredLength;

        if (string.IsNullOrWhiteSpace(password) || password.Length < requiredLength)
        {
            errors ??= [];
            errors.Add(Describer.PasswordTooShort(requiredLength));
        }

        bool requireNonAlphanumeric = passwordOptions is not null
                  ? passwordOptions.RequireNonAlphanumeric
                  : manager.Options.Password.RequireNonAlphanumeric;

        if (requireNonAlphanumeric && password.All(IsLetterOrDigit))
        {
            errors ??= [];
            errors.Add(Describer.PasswordRequiresNonAlphanumeric());
        }

        bool requireDigit = passwordOptions is not null
                  ? passwordOptions.RequireDigit
                  : manager.Options.Password.RequireDigit;

        if (requireDigit && !password.Any(IsDigit))
        {
            errors ??= [];
            errors.Add(Describer.PasswordRequiresDigit());
        }

        bool requireLowercase = passwordOptions is not null
                  ? passwordOptions.RequireLowercase
                  : manager.Options.Password.RequireLowercase;

        if (requireLowercase && !password.Any(IsLower))
        {
            errors ??= [];
            errors.Add(Describer.PasswordRequiresLower());
        }

        bool requireUppercase = passwordOptions is not null
                ? passwordOptions.RequireUppercase
                : manager.Options.Password.RequireUppercase;

        if (requireUppercase && !password.Any(IsUpper))
        {
            errors ??= [];
            errors.Add(Describer.PasswordRequiresUpper());
        }

        int requiredUniqueChars = passwordOptions is not null
                ? passwordOptions.RequiredUniqueChars
                : manager.Options.Password.RequiredUniqueChars;

        if (requiredUniqueChars >= 1 && password.Distinct().Count() < requiredUniqueChars)
        {
            errors ??= [];
            errors.Add(Describer.PasswordRequiresUniqueChars(requiredUniqueChars));
        }

        return
            await Task.FromResult(errors?.Count > 0
                ? IdentityResult.Failed([.. errors])
                : IdentityResult.Success);
    }
}
