using System;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// interface de Tenant.
/// </summary>
public interface IEntityTenant
{
    /// <summary>
    /// Id do tenant.
    /// </summary>
    public Guid TenantId { get; set; }
}
