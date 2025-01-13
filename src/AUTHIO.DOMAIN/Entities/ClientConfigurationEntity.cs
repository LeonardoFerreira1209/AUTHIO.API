namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de configuração de Clienty.
/// </summary>
public class ClientConfigurationEntity : IEntityPrimaryKey<Guid>, IEntityClient
{
    /// <summary>
    /// ctor
    /// </summary>
    public ClientConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="clientKey"></param>
    /// <param name="clientId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    public ClientConfigurationEntity(
        string clientKey, Guid clientId,
        DateTime created, DateTime? updated,
        ClientIdentityConfigurationEntity clientIdentityConfiguration,
        ClientEmailConfigurationEntity clientEmailConfiguration,
        ClientTokenConfigurationEntity clientTokenConfiguration)
    {
        ClientKey = clientKey;
        ClientId = clientId;
        Created = created;
        Updated = updated;
        ClientIdentityConfiguration = clientIdentityConfiguration;
        ClientEmailConfiguration = clientEmailConfiguration;
        ClientTokenConfiguration = clientTokenConfiguration;
    }

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
    /// Entidade do Client.
    /// </summary>
    public virtual ClientEntity Client { get; private set; }

    /// <summary>
    /// Entidade do Client identity configuration.
    /// </summary>
    public virtual ClientIdentityConfigurationEntity ClientIdentityConfiguration { get; set; }

    /// <summary>
    /// Entidade do Client email configuration.
    /// </summary>
    public virtual ClientEmailConfigurationEntity ClientEmailConfiguration { get; set; }

    /// <summary>
    /// Entidade do Client token configuration.
    /// </summary>
    public virtual ClientTokenConfigurationEntity ClientTokenConfiguration { get; set; }

    /// <summary>
    /// Chave de acesso do Client.
    /// </summary>
    public string ClientKey { get; set; }
}
