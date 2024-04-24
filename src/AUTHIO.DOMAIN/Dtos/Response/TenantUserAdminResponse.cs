namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de user admin.
/// </summary>
public class TenantUserAdminResponse
{
    /// <summary>
    /// User Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Dados do usuário.
    /// </summary>
    public UserResponse User { get; set; }

    /// <summary>
    /// Id do tenant responsavel.
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Tenant.
    /// </summary>
    public TenantResponse Tenant { get; set; }
}
