namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de vinculo entre usuário admin e tenant.
/// </summary>
public class TenantEmailConfigurationEntity : IEntityPrimaryKey<Guid>
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
    public string SerdersName { get; set; }

    /// <summary>
    /// Email do Remetente.
    /// </summary>
    public string SendersEmail { get; set; }

    /// <summary>
    /// Email do Remetente confirmado.
    /// </summary>
    public bool IsEmailConfirmed { get; set; }  
    
    /// <summary>
    /// Id do template do Email.
    /// </summary>
    public string TemplateId { get; set; }

    /// <summary>
    /// Id do tenant configuration Id.
    /// </summary>
    public Guid TenantConfigurationId { get; set; }

    /// <summary>
    /// Entidade do tenant configuration.
    /// </summary>
    public virtual TenantConfigurationEntity TenantConfiguration { get; set; }

}
