namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Interface de Tenant.
/// </summary>
public interface IEntityTenant
{
    /// <summary>
    /// Id do tenant.
    /// </summary>
    public Guid TenantId { get; }
}
