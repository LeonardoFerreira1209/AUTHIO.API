using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace AUTHIO.INFRASTRUCTURE.Services.Identity;

/// <summary>
/// Classe customizada do User Manager do Identity.
/// </summary>
/// <typeparam name="TUser"></typeparam>
/// <param name="store"></param>
/// <param name="optionsAccessor"></param>
/// <param name="passwordHasher"></param>
/// <param name="userValidators"></param>
/// <param name="passwordValidators"></param>
/// <param name="keyNormalizer"></param>
/// <param name="errors"></param>
/// <param name="services"></param>
/// <param name="logger"></param>
public class CustomUserManager<TUser>(
    ICustomUserStore<TUser> store,
    IOptions<IdentityOptions> optionsAccessor,
    IPasswordHasher<TUser> passwordHasher,
    IEnumerable<CustomUserValidator<TUser>> userValidators,
    IEnumerable<CustomPasswordValidator<TUser>> passwordValidators,
    ILookupNormalizer keyNormalizer, CustomIdentityErrorDescriber errors,
    IServiceProvider services,
    ILogger<UserManager<TUser>> logger)
        : UserManager<TUser>(store, optionsAccessor, passwordHasher,
            userValidators, passwordValidators, keyNormalizer,
                errors, services, logger) where TUser : UserEntity, new()
{

    /// <summary>
    /// Provedor de serviços.
    /// </summary>
    private readonly IServiceProvider _services = services;

    /// <summary>
    /// Instancia de ICustomUserStore.
    /// </summary>
    protected new internal ICustomUserStore<TUser> Store { get; set; }
        = store ?? throw new ArgumentNullException(nameof(store));

    /// <summary>
    /// Cria um usuário com senha. 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public override async Task<IdentityResult> CreateAsync(
        TUser user, string password)
    {
        ThrowIfDisposed();

        var passwordStore
            = GetPasswordStore();

        return await UpdatePasswordHash(
            passwordStore, user, password).ContinueWith(
                async (taskIdentityResult) =>
                {
                    var identityResult
                        = taskIdentityResult.Result;

                    if (!identityResult.Succeeded) return identityResult;

                    return await CreateAsync(user);

                }
            ).Unwrap();
    }

    /// <summary>
    /// Cria um usuário.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public override async Task<IdentityResult> CreateAsync(TUser user)
    {
        ThrowIfDisposed();

        return await UpdateSecurityStampInternal(user).ContinueWith(
            async (task) =>
            {
                return await ValidateUserAsync(user).ContinueWith(
                    async (taskIdentityResult) =>
                    {
                        var identityResult
                            = taskIdentityResult.Result;

                        if (!identityResult.Succeeded) return identityResult;

                        if (Options.Lockout.AllowedForNewUsers && SupportsUserLockout)
                            await GeTUserLockoutStore()
                                    .SetLockoutEnabledAsync(
                                         user, true, CancellationToken);

                        await UpdateNormalizedUserNameAsync(user);
                        await UpdateNormalizedEmailAsync(user);

                        return await Store
                            .CreateAsync(
                                user, CancellationToken);

                    }).Unwrap();

            }).Result;
    }

    /// <summary>
    /// Busca um usuário pelo nome e expressão.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public async Task<TUser> FindByNameWithExpressionAsync(
        string userName, 
        Expression<Func<TUser, bool>> expression
        )
    {
        ThrowIfDisposed();
        userName = NormalizeName(userName);

        var user = await Store.FindByNameWithExpressionAsync(
            userName, expression, CancellationToken).ConfigureAwait(false);

        if (user == null 
            && Options.Stores.ProtectPersonalData)
        {
            var keyRing = _services.GetService<ILookupProtectorKeyRing>();
            var protector = _services.GetService<ILookupProtector>();

            if (keyRing != null 
                && protector != null)
            {
                foreach (var key in keyRing.GetAllKeyIds())
                {
                    var oldKey = protector.Protect(key, userName);

                    user = await Store.FindByNameWithExpressionAsync(
                        oldKey, expression, CancellationToken).ConfigureAwait(false);

                    if (user != null)
                    {
                        return user;
                    }
                }
            }
        }

        return user;
    }

    /// <summary>
    /// Busca o usuário pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TUser> GetUserByIdAsync(
        Guid id, 
        Expression<Func<TUser, bool>> expression,
        CancellationToken cancellationToken
    ) 
    => await Store.FindByIdWithExpressionAsync(
        id,
        expression,
        cancellationToken
    );

    /// <summary>
    /// Valida os dados do usuário.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected new async Task<IdentityResult> ValidateUserAsync(TUser user)
    {
        if (SupportsUserSecurityStamp)
            if (string.IsNullOrEmpty(
                await GetSecurityStampAsync(user))) throw new InvalidOperationException("Security Stamp não pode ser nulo.");

        List<IdentityError> errors = null;
        foreach (var validator in UserValidators)
        {
            await validator.ValidateAsync(this, user).ContinueWith(
                (taskIdentityResult) =>
                {
                    var result = taskIdentityResult.Result;
                    if (!result.Succeeded)
                    {
                        errors ??= [];
                        errors.AddRange(result.Errors);
                    }
                }
            );
        }

        if (errors?.Count > 0)
            return IdentityResult.Failed([.. errors]);

        return IdentityResult.Success;
    }

    /// <summary>
    /// Atualiza o hash da senha.
    /// </summary>
    /// <param name="passwordStore"></param>
    /// <param name="user"></param>
    /// <param name="newPassword"></param>
    /// <param name="validatePassword"></param>
    /// <returns></returns>
    private async Task<IdentityResult> UpdatePasswordHash(IUserPasswordStore<TUser> passwordStore,
        TUser user, string newPassword, bool validatePassword = true)
    {
        if (validatePassword)
        {
            var validate
                = await ValidatePasswordAsync(user, newPassword);

            if (!validate.Succeeded) return validate;
        }

        var hash =
            newPassword != null
                ? PasswordHasher.HashPassword(
                    user, newPassword)
                : null;

        return await passwordStore.SetPasswordHashAsync(
            user, hash, CancellationToken).ContinueWith(
                async (task) =>
                {
                    await UpdateSecurityStampInternal(user);

                    return IdentityResult.Success;

                }).Result;
    }

    /// <summary>
    /// Atualiza o Security stamp interno.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task UpdateSecurityStampInternal(
        TUser user)
    {
        if (SupportsUserSecurityStamp)
        {
            await GetSecurityStore()
                .SetSecurityStampAsync(
                    user, Base32.GenerateBase32(), CancellationToken);
        }
    }

    /// <summary>
    /// Cria uma instancia de IUserPasswordStore.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    private IUserPasswordStore<TUser> GetPasswordStore()
    {
        IUserPasswordStore<TUser> cast
            = Store as IUserPasswordStore<TUser>;

        return cast
            ?? throw new NotSupportedException();
    }

    /// <summary>
    /// Cria uma instancia de IUserSecurityStampStore.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    private IUserSecurityStampStore<TUser> GetSecurityStore()
    {
        IUserSecurityStampStore<TUser> cast
            = Store as IUserSecurityStampStore<TUser>;

        return cast
            ?? throw new NotSupportedException();
    }

    /// <summary>
    /// Cria uma instancia de IUserLockoutStore.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    private IUserLockoutStore<TUser> GeTUserLockoutStore()
    {
        IUserLockoutStore<TUser> cast
            = Store as IUserLockoutStore<TUser>;

        return cast
            ?? throw new NotSupportedException();
    }
}
