using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Dtos.Response;

/// <summary>
/// Classe de response de Tenant.
/// </summary>
public class TenantResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; } = null;

    /// <summary>
    /// Status.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Nomde do tenant.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Descrição do tenant.
    /// </summary>
    public string Description { get; set; } = null;

    /// <summary>
    /// Users vinculados ao tenant.
    /// </summary>
    public ICollection<UserResponse> Users { get; set; }

    /// <summary>
    /// Users Admins vinculados ao tenant.
    /// </summary>
    public ICollection<TenantUserAdminResponse> UserAdmins { get; set; }

    /// <summary> 
    /// Roles vinculadas ao tenant.
    /// </summary>
    public ICollection<RoleEntity> Roles { get; set; }
}
