using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AUTHIO.APPLICATION.Application.Services.Custom;

/// <summary>
/// Classe customizada de validação de usuários.
/// </summary>
public class CustomUserValidator<TUser>
    : UserValidator<TUser> where TUser : class
{
    /// <summary>
    /// Valida o usuário.
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public override async Task<IdentityResult> ValidateAsync(
        UserManager<TUser> manager, TUser user)
    {
        return await ValidateUserName(manager, user).ContinueWith(
            async (taskIdentityError) =>
            {
                var identityErrors 
                    = taskIdentityError.Result;

                if (manager.Options.User.RequireUniqueEmail)
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
    /// <returns></returns>
    private async Task<List<IdentityError>> ValidateUserName(
        UserManager<TUser> manager, TUser user)
    {
        List<IdentityError> errors = null;

        return await manager.GetUserNameAsync(user).ContinueWith(
            async (taskString) =>
            {
                var userName 
                    = taskString.Result;

                if (string.IsNullOrWhiteSpace(userName)) {

                    errors ??= [];
                    errors.Add(Describer.InvalidUserName(userName));
                }
                else if (!string.IsNullOrEmpty(
                    manager.Options.User.AllowedUserNameCharacters) 
                        && userName.Any(
                            c => !manager.Options.User.AllowedUserNameCharacters.Contains(c))) {

                    errors ??= [];
                    errors.Add(Describer.InvalidUserName(userName));
                }
                else
                {
                    var owner = await manager.FindByNameAsync(userName);

                    if (owner != null &&
                        !string.Equals(await manager.GetUserIdAsync(
                            owner), await manager.GetUserIdAsync(user))) {

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

                if (string.IsNullOrWhiteSpace(email)) {

                    errors ??= [];
                    errors.Add(Describer.InvalidEmail(email));
                    return errors;
                }

                if (!new EmailAddressAttribute().IsValid(email)) {

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
