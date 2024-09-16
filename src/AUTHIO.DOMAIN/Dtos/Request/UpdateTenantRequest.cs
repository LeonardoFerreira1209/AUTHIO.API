namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de Tenant.
/// </summary>
public sealed class UpdateTenantRequest
{
    /// <summary>
    /// Id do tenant.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Nomde do tenant.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Descrição do tenant.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Classe de atualização de configuração de tenant.
    /// </summary>
    public UpdateTenantConfigurationRequest TenantConfiguration { get; set; }
}
