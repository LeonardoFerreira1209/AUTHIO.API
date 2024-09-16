namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de tenant email configuration.
/// </summary>
public sealed class UpdateTenantEmailConfigurationRequest
{
    /// <summary>
    /// Id do tenant.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Nome do Remetente.
    /// </summary>
    public string SendersName { get; set; }

    /// <summary>
    /// Email do Remetente.
    /// </summary>
    public string SendersEmail { get; set; }

    /// <summary>
    /// Email do Remetente confirmado.
    /// </summary>
    public bool IsEmailConfirmed { get; set; }

    /// <summary>
    /// Atualização de SendGrid Configurations.
    /// </summary>
    public UpdateSendGridConfigurationRequest SendGridConfiguration { get; set; }
}
