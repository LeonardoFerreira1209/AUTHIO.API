namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de Configuração de email do Client.
/// </summary>
public class ClientEmailConfigurationEntity : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public ClientEmailConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="clientConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="serdersName"></param>
    /// <param name="serdersEmail"></param>
    /// <param name="isEmailConfirmed"></param>
    public ClientEmailConfigurationEntity(
        Guid clientConfigurationId, DateTime created,
        DateTime? updated, string serdersName, string serdersEmail,
        bool isEmailConfirmed,
        SendGridConfigurationEntity sendGridConfiguration)
    {
        ClientConfigurationId = clientConfigurationId;
        Created = created;
        Updated = updated;
        SendersName = serdersName;
        SendersEmail = serdersEmail;
        IsEmailConfirmed = isEmailConfirmed;
        SendGridConfiguration = sendGridConfiguration;
    }

    /// <summary>
    /// Id
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
    /// Entidade do SendGrid configuration.
    /// </summary>
    public virtual SendGridConfigurationEntity SendGridConfiguration {  get; set; }

    /// <summary>
    /// Id do Client configuration Id.
    /// </summary>
    public Guid ClientConfigurationId { get; set; }

    /// <summary>
    /// Entidade do Client configuration.
    /// </summary>
    public virtual ClientConfigurationEntity ClientConfiguration { get; private set; }

}
