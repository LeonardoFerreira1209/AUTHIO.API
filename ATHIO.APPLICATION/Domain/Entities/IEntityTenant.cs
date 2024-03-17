namespace AUTHIO.APPLICATION.Domain.Entities;

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
