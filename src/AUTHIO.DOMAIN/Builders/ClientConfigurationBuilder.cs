using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de ClientConfiguration.
/// </summary>
public sealed class ClientConfigurationBuilder
{
    private string ClientKey;
    private Guid ClientId;
    private DateTime? updated = null;
    private DateTime created;
    private ClientIdentityConfigurationEntity ClientIdentityConfiguration;
    private ClientEmailConfigurationEntity ClientEmailConfiguration;
    private ClientTokenConfigurationEntity ClientTokenConfiguration;

    /// <summary>
    /// Adiciona uma ClientKey.
    /// </summary>
    /// <param name="ClientKey"></param>
    /// <returns></returns>
    public ClientConfigurationBuilder AddClientKey(string ClientKey)
    {
        this.ClientKey = ClientKey;

        return this;
    }

    /// <summary>
    /// Adiciona um ClientId.
    /// </summary>
    /// <param name="ClientId"></param>
    /// <returns></returns>
    public ClientConfigurationBuilder AddClientId(Guid ClientId)
    {
        this.ClientId = ClientId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public ClientConfigurationBuilder AddCreated(DateTime? created = null)
    {
        this.created
            = created
            ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona a data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    public ClientConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona Client identity configuration.
    /// </summary>
    /// <param name="ClientIdentityConfiguration"></param>
    /// <returns></returns>
    public ClientConfigurationBuilder AddClientIdentityConfiguration(ClientIdentityConfigurationEntity ClientIdentityConfiguration)
    {
        this.ClientIdentityConfiguration = ClientIdentityConfiguration;

        return this;
    }

    /// <summary>
    /// Adiciona Client email configuration.
    /// </summary>
    /// <param name="ClientEmailConfiguration"></param>
    /// <returns></returns>
    public ClientConfigurationBuilder AddClientEmailConfiguration(ClientEmailConfigurationEntity ClientEmailConfiguration)
    {
        this.ClientEmailConfiguration = ClientEmailConfiguration;

        return this;
    }

    /// <summary>
    /// Adiciona Client token configuration.
    /// </summary>
    /// <param name="ClientTokenConfiguration"></param>
    /// <returns></returns>
    public ClientConfigurationBuilder AddClientTokenConfiguration(ClientTokenConfigurationEntity ClientTokenConfiguration)
    {
        this.ClientTokenConfiguration = ClientTokenConfiguration;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public ClientConfigurationEntity Builder()
        => new(ClientKey, ClientId, 
            created, updated,
            ClientIdentityConfiguration, 
            ClientEmailConfiguration, 
            ClientTokenConfiguration
        );
}
