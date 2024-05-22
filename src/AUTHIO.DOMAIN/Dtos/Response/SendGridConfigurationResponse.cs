namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de SendGridConfiguration.
/// </summary>
public class SendGridConfigurationResponse
{
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
    public TenantEmailConfigurationResponse TenantEmailConfiguration { get; set; }
}
