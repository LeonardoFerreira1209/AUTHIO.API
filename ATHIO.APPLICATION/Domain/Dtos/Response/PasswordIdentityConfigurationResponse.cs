using Microsoft.AspNetCore.Identity;

namespace AUTHIO.APPLICATION.Domain.Dtos.Response;

/// <summary>
/// Classe de response de PasswordIdentityConfiguration.
/// </summary>
public class PasswordIdentityConfigurationResponse : PasswordOptions
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
    /// Id do tenant identity configuration.
    /// </summary>
    public Guid TenantIdentityConfigurationId { get; set; }

    /// <summary>
    /// Entidade do tenant identity configuration.
    /// </summary>
    public virtual TenantIdentityConfigurationResponse TenantIdentityConfiguration { get; set; }
}
