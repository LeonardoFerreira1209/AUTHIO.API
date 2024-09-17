namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de Configuração de email do tenant.
/// </summary>
public class TenantEmailConfigurationEntity : IEntityPrimaryKey<Guid>
{
    /// <summary>
    /// ctor
    /// </summary>
    public TenantEmailConfigurationEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tenantConfigurationId"></param>
    /// <param name="created"></param>
    /// <param name="updated"></param>
    /// <param name="serdersName"></param>
    /// <param name="serdersEmail"></param>
    /// <param name="isEmailConfirmed"></param>
    public TenantEmailConfigurationEntity(
        Guid tenantConfigurationId, DateTime created,
        DateTime? updated, string serdersName, string serdersEmail,
        bool isEmailConfirmed,
        SendGridConfigurationEntity sendGridConfiguration)
    {
        TenantConfigurationId = tenantConfigurationId;
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
    /// Id do tenant configuration Id.
    /// </summary>
    public Guid TenantConfigurationId { get; set; }

    /// <summary>
    /// Entidade do tenant configuration.
    /// </summary>
    public virtual TenantConfigurationEntity TenantConfiguration { get; set; }

}
