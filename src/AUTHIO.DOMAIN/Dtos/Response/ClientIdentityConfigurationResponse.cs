namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de ClientIdentityConfiguration.
/// </summary>
public class ClientIdentityConfigurationResponse
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
    /// Id do Client configuration Id.
    /// </summary>
    public Guid ClientConfigurationId { get; set; }

    /// <summary>
    /// Entidade do Client configuration.
    /// </summary>
    public ClientConfigurationResponse ClientConfiguration { get; set; }

    /// <summary>
    /// Entidade de user identity configuration.
    /// </summary>
    public UserIdentityConfigurationResponse UserIdentityConfiguration { get; set; }

    /// <summary>
    /// Entidade de password identity configuration.
    /// </summary>
    public PasswordIdentityConfigurationResponse PasswordIdentityConfiguration { get; set; }

    /// <summary>
    /// Entidade de lockout identity configuration.
    /// </summary>
    public LockoutIdentityConfigurationResponse LockoutIdentityConfiguration { get; set; }
}
