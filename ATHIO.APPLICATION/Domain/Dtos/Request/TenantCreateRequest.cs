namespace AUTHIO.APPLICATION.Domain.Dtos.Request;

/// <summary>
/// Request de criação de Tenant.
/// </summary>
public sealed class TenantCreateRequest
{
    /// <summary>
    /// Nomde do tenant.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Descrição do tenant.
    /// </summary>
    public string Description { get; set; } = null;

    /// <summary>
    /// Dados do admin do Tenant (Responsável).
    /// </summary>
    public TenantUserAdminRequest UserAdmin { get; set; }
}

/// <summary>
/// Record de dados do Usuário de criação do Tenant.
/// </summary>
/// <param name="Name"></param>
/// <param name="LastName"></param>
/// <param name="Username"></param>
/// <param name="Email"></param>
/// <param name="PhoneNumber"></param>
/// <param name="Password"></param>
public record TenantUserAdminRequest(
        string Name, string LastName, 
            string Username, string Email, 
                string PhoneNumber, string Password);
