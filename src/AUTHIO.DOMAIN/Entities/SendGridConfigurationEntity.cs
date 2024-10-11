namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de vinculo entre usuário admin e tenant.
/// </summary>
public class SendGridConfigurationEntity : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public SendGridConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantEmailConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="sendGridApiKey"></param>
    /// <param name="welcomeTemplateId"></param>
    public SendGridConfigurationEntity(
        Guid tenantEmailConfigurationId, DateTime created,
        DateTime? updated, string sendGridApiKey, string welcomeTemplateId)
    {
        TenantEmailConfigurationId = tenantEmailConfigurationId;
        Created = created;
        Updated = updated;
        SendGridApiKey = sendGridApiKey;
        WelcomeTemplateId = welcomeTemplateId;
    }

    /// <summary>
    /// User Id
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
    /// Chave de Api do SendGrid.
    /// </summary>
    public string SendGridApiKey { get; set; } = null;

    /// <summary>
    /// Id do template de bem vindo e confirmação de email do usuário.
    /// </summary>
    public string WelcomeTemplateId { get; set; } = null;

    /// <summary>
    /// Id do tenant email configuration Id.
    /// </summary>
    public Guid TenantEmailConfigurationId { get; set; }

    /// <summary>
    /// Entidade do tenant email configuration.
    /// </summary>
    public virtual TenantEmailConfigurationEntity TenantEmailConfiguration { get; private set; }
}
