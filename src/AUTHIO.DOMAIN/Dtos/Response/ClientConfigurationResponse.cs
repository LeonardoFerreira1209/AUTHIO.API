namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de ClientConfiguration.
/// </summary>
public class ClientConfigurationResponse
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
    /// Id do Client.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Response do Client.
    /// </summary>
    public ClientResponse Client { get; set; }

    /// <summary>
    /// Response do Client identity configuration.
    /// </summary>
    public ClientIdentityConfigurationResponse ClientIdentityConfiguration { get; set; }
    
    /// <summary>
    /// Response de Configuraçoes de email.
    /// </summary>
    public ClientEmailConfigurationResponse ClientEmailConfiguration { get; set; }

    /// <summary>
    /// Response de Configuraçoes de token.
    /// </summary>
    public ClientTokenConfigurationResponse ClientTokenConfiguration { get; set; }

    /// <summary>
    /// Chave de acesso.
    /// </summary>
    public string ClientKey { get; set; }
}
