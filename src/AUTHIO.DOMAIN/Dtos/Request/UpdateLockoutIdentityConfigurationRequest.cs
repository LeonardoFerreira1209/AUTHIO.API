namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de Lockout.
/// </summary>
public sealed class UpdateLockoutIdentityConfigurationRequest
{
    /// <summary>
    /// Id do Client.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Habilitado para novos usuários.
    /// </summary>
    public bool AllowedForNewUsers { get; set; }

    /// <summary>
    /// Número máximo de tentativas.
    /// </summary>
    public int MaxFailedAccessAttempts { get; set; }

    /// <summary>
    /// Tempo de espera padrão.
    /// </summary>
    public TimeSpan DefaultLockoutTimeSpan { get; set; }
}
