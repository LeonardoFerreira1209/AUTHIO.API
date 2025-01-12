using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de ClientEmailConfiguration.
/// </summary>
public sealed class ClientEmailConfigurationBuilder
{
    /// <summary>
    /// Private properties.
    /// </summary>
    private Guid ClientConfigurationId;
    private string serdersName;
    private string serdersEmail;
    private bool isEmailConfirmed;
    private DateTime created;
    private DateTime? updated = null;
    private SendGridConfigurationEntity sendGridConfiguration;

    /// <summary>
    /// Adiciona um Client configuration Id.
    /// </summary>
    /// <param name="ClientConfigurationId"></param>
    /// <returns></returns>
    public ClientEmailConfigurationBuilder AddClientConfigurationId(Guid ClientConfigurationId)
    {
        this.ClientConfigurationId = ClientConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public ClientEmailConfigurationBuilder AddCreated(DateTime? created = null)
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
    public ClientEmailConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona o nome do remetente.
    /// </summary>
    /// <param name="serdersName"></param>
    /// <returns></returns>
    public ClientEmailConfigurationBuilder AddSendersName(string serdersName)
    {
        this.serdersName = serdersName;

        return this;
    }

    /// <summary>
    /// Adiciona o email do remetente.
    /// </summary>
    /// <param name="serdersEmail"></param>
    /// <returns></returns>
    public ClientEmailConfigurationBuilder AddSendersEmail(string serdersEmail)
    {
        this.serdersEmail = serdersEmail;

        return this;
    }

    /// <summary>
    /// Adiciona se o email está confirmado.
    /// </summary>
    /// <param name="isEmailConfirmed"></param>
    /// <returns></returns>
    public ClientEmailConfigurationBuilder AddIsEmailConfirmed(bool isEmailConfirmed)
    {
        this.isEmailConfirmed = isEmailConfirmed;

        return this;
    }

    /// <summary>
    /// Adiciona configurações do sendGrid.
    /// </summary>
    /// <param name="sendGridConfiguration"></param>
    /// <returns></returns>
    public ClientEmailConfigurationBuilder AddSendGridConfiguration(SendGridConfigurationEntity sendGridConfiguration)
    {
        this.sendGridConfiguration = sendGridConfiguration;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public ClientEmailConfigurationEntity Builder()
        => new(ClientConfigurationId, created,
            updated, serdersName,
            serdersEmail, 
            isEmailConfirmed, 
            sendGridConfiguration
        );
}
