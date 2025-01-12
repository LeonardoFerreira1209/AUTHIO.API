namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de sendGrid configuration.
/// </summary>
public sealed class UpdateSendGridConfigurationRequest
{
    /// <summary>
    /// Id do Client.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Chave de Api do SendGrid.
    /// </summary>
    public string SendGridApiKey { get; set; }

    /// <summary>
    /// Id do template de bem vindo e confirmação de email do usuário.
    /// </summary>
    public string WelcomeTemplateId { get; set; }
}
