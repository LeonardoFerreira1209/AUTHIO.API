namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de Client confiurations.
/// </summary>
public sealed class UpdateClientConfigurationRequest
{
    /// <summary>
    /// Id .
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Atualização Client identity configuration.
    /// </summary>
    public UpdateClientIdentityConfigurationRequest ClientIdentityConfiguration { get; set; }

    /// <summary>
    /// Atualização Client email configuration
    /// </summary>
    public UpdateClientEmailConfigurationRequest ClientEmailConfiguration { get; set; }

    /// <summary>
    /// Atualização do Client token configuration.
    /// </summary>
    public UpdateClientTokenConfigurationRequest ClientTokenConfiguration { get; set; }
}
