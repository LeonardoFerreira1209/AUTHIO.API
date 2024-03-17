namespace AUTHIO.APPLICATION.Domain.Entities;

/// <summary>
/// Interface de Tenant nullable.
/// </summary>
public interface IEntityTenantNullAble
{
    /// <summary>
    /// Id do tenant.
    /// </summary>
    public Guid? TenantId { get; }
}
