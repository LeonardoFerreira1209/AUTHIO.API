using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Dtos.Response;

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
    /// Id do Client identity configuration.
    /// </summary>
    public Guid ClientIdentityConfigurationId { get; set; }

    /// <summary>
    /// Entidade do Client identity configuration.
    /// </summary>
    public ClientIdentityConfigurationResponse ClientIdentityConfiguration { get; set; }
}
