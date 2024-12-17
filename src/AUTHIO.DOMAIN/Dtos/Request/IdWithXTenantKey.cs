using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Busca dados por Id e X-Tenant-Key.
/// </summary>
public class IdWithXTenantKey
{
    /// <summary>
    /// Id de referência.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Chave do tenant.
    /// </summary>
    [FromHeader(Name = "X-Tenant-KEY")]
    public string TenantKey { get; set; }
}
