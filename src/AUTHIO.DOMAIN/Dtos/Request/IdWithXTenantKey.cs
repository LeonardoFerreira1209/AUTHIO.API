using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Busca dados por Id e x-tenant-key.
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
    [FromHeader(Name = "x-tenant-key")]
    public string TenantKey { get; set; }
}
