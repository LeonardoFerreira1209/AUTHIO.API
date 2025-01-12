namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de ClientEmailConfiguration.
/// </summary>
public class ClientEmailConfigurationResponse
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
    /// Id do Client configuration Id.
    /// </summary>
    public Guid ClientConfigurationId { get; set; }

    /// <summary>
    /// Response do sendgrid configuration.
    /// </summary>
    public SendGridConfigurationResponse SendGridConfiguration { get; set; }

    /// <summary>
    /// Response do Client configuration.
    /// </summary>
    public ClientConfigurationResponse ClientConfiguration { get; set; }
}
