namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de TenantConfiguration.
/// </summary>
public class TenantConfigurationResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// Id do tenant.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Response do tenant.
    /// </summary>
    public TenantResponse Tenant { get; set; }

    /// <summary>
    /// Response do tenant identity configuration.
    /// </summary>
    public TenantIdentityConfigurationResponse TenantIdentityConfiguration { get; set; }

    /// <summary>
    /// Chave de acesso.
    /// </summary>
    public string TenantKey { get; set; }
}
