namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de user identity confiurations.
/// </summary>
public sealed class UpdateUserIdentityConfigurationRequest
{
    /// <summary>
    /// Id.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Requer um e-mail unico.
    /// </summary>
    public required bool RequireUniqueEmail {  get; set; }

    /// <summary>
    /// Caracteres habilitados para nome de usuário.
    /// </summary>
    public required string AllowedUserNameCharacters { get; set; }
}
