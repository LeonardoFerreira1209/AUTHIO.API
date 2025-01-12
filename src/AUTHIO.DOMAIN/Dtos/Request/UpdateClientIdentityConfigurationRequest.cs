namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de Client identity confiurations.
/// </summary>
public sealed class UpdateClientIdentityConfigurationRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Dados de atualização de user identity.
    /// </summary>
    public UpdateUserIdentityConfigurationRequest UserIdentityConfiguration { get; set; }

    /// <summary>
    /// Dados de atualização de senha do identity.
    /// </summary>
    public UpdatePasswordIdentityConfigurationRequest PasswordIdentityConfiguration { get; set; }

    /// <summary>
    /// Dados de atualização de lockout do identity.
    /// </summary>
    public UpdateLockoutIdentityConfigurationRequest LockoutIdentityConfiguration { get; set; }
}
