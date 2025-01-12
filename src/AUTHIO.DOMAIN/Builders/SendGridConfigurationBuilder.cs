using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de SendGridConfiguration.
/// </summary>
public sealed class SendGridConfigurationBuilder
{
    private Guid ClientEmailConfigurationId;
    private string sendGridApiKey = null;
    private string welcomeTemplateId = null;
    private DateTime created;
    private DateTime? updated = null;

    /// <summary>
    /// Adiciona um Client email configuration Id.
    /// </summary>
    /// <param name="ClientEmailConfigurationId"></param>
    /// <returns></returns>
    public SendGridConfigurationBuilder AddClientEmailConfigurationId(Guid ClientEmailConfigurationId)
    {
        this.ClientEmailConfigurationId = ClientEmailConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public SendGridConfigurationBuilder AddCreated(DateTime? created = null)
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
    public SendGridConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona a apikey do sendGrid.
    /// </summary>
    /// <param name="sendGridApiKey"></param>
    /// <returns></returns>
    public SendGridConfigurationBuilder AddSendGridApiKey(string sendGridApiKey)
    {
        this.sendGridApiKey = sendGridApiKey;

        return this;
    }

    /// <summary>
    /// Adiciona o id do template de bem vindo e confimacao de usuario.
    /// </summary>
    /// <param name="welcomeTemplateId"></param>
    /// <returns></returns>
    public SendGridConfigurationBuilder AddWelcomeTemplateId(string welcomeTemplateId)
    {
        this.welcomeTemplateId = welcomeTemplateId;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public SendGridConfigurationEntity Builder()
        => new(ClientEmailConfigurationId, created,
            updated, sendGridApiKey,
            welcomeTemplateId);
}
