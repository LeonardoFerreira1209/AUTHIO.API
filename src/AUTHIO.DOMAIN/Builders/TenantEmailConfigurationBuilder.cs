using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de TenantEmailConfiguration.
/// </summary>
public class TenantEmailConfigurationBuilder
{
    private Guid tenantConfigurationId;
    private string serdersName;
    private string serdersEmail;
    private bool isEmailConfirmed;
    private DateTime created;
    private DateTime? updated = null;

    /// <summary>
    /// Adiciona um Tenant configuration Id.
    /// </summary>
    /// <param name="tenantConfigurationId"></param>
    /// <returns></returns>
    public TenantEmailConfigurationBuilder AddTenantConfigurationId(Guid tenantConfigurationId)
    {
        this.tenantConfigurationId = tenantConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public TenantEmailConfigurationBuilder AddCreated(DateTime? created = null)
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
    public TenantEmailConfigurationBuilder AddUpdated(DateTime? updated = null)
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
    public TenantEmailConfigurationBuilder AddSendersName(string serdersName)
    {
        this.serdersName = serdersName;

        return this;
    }

    /// <summary>
    /// Adiciona o email do remetente.
    /// </summary>
    /// <param name="serdersEmail"></param>
    /// <returns></returns>
    public TenantEmailConfigurationBuilder AddSendersEmail(string serdersEmail)
    {
        this.serdersEmail = serdersEmail;

        return this;
    }

    /// <summary>
    /// Adiciona se o email está confirmado.
    /// </summary>
    /// <param name="isEmailConfirmed"></param>
    /// <returns></returns>
    public TenantEmailConfigurationBuilder AddIsEmailConfirmed(bool isEmailConfirmed)
    {
        this.isEmailConfirmed = isEmailConfirmed;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public TenantEmailConfigurationEntity Builder()
        => new(tenantConfigurationId, created,
            updated, serdersName,
            serdersEmail, isEmailConfirmed);
}
