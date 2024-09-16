namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de Tenant confiurations.
/// </summary>
public sealed class UpdateTenantConfigurationRequest
{
    /// <summary>
    /// Id .
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Atualização Tenant identity configuration.
    /// </summary>
    public UpdateTenantIdentityConfigurationRequest TenantIdentityConfiguration { get; set; }

    /// <summary>
    /// Atualização Tenant email configuration
    /// </summary>
    public UpdateTenantEmailConfigurationRequest TenantEmailConfiguration { get; set; }

    /// <summary>
    /// Atualização do tenant token configuration.
    /// </summary>
    public UpdateTenantTokenConfigurationRequest TenantTokenConfiguration { get; set; }
}
