using System;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// interfpORpOace de Tenant.
/// </summary>
public interface IEntityTenant
{
    /// <summary>
    /// Id do tenant.
    /// </summary>
    public Guid? TenantId { get; set; }
}
