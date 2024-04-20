namespace AUTHIO.APPLICATION.Domain.Dtos.Request;

/// <summary>
/// Request de criação de Tenant.
/// </summary>
public sealed class CreateTenantRequest
{
    /// <summary>
    /// Nomde do tenant.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Descrição do tenant.
    /// </summary>
    public string Description { get; set; } = null;
}
