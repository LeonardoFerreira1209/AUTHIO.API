using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Builder de tenant.
/// </summary>
public sealed class TenantBuilder
{
    /// <summary>
    /// private properties
    /// </summary>
    private Guid userId;
    private DateTime created;
    private DateTime? updated = null;
    private Status status;
    private string name;
    private string description;
    private TenantConfigurationEntity tenantConfiguration;

    /// <summary>
    /// Adiciona o id do usuario.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public TenantBuilder AddUserId(Guid userId)
    {
        this.userId = userId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public TenantBuilder AddCreated(DateTime? created = null)
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
    public TenantBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Adiciona o status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public TenantBuilder AddStatus(Status status)
    {
        this.status = status;

        return this;
    }

    /// <summary>
    /// Adiciona o nome.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public TenantBuilder AddName(string name)
    {
        this.name = name;

        return this;
    }

    /// <summary>
    /// Adiciona a descrição.
    /// </summary>
    /// <param name="description"></param>
    /// <returns></returns>
    public TenantBuilder AddDescription(string description)
    {
        this.description = description;

        return this;
    }

    /// <summary>
    /// Add Tenant Configuration.
    /// </summary>
    /// <param name="tenantConfiguration"></param>
    /// <returns></returns>
    public TenantBuilder AddTenantConfiguration(TenantConfigurationEntity tenantConfiguration)
    {
        this.tenantConfiguration = tenantConfiguration;

        return this;
    }

    /// <summary>
    /// Builder
    /// </summary>
    /// <returns></returns>
    public TenantEntity Builder() =>
        new(userId,
            created, updated,
            status, name,
            description, tenantConfiguration
        );
}
