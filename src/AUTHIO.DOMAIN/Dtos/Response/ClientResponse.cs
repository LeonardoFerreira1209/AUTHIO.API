using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de Client.
/// </summary>
public class ClientResponse
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
    /// Nomde do Client.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Descrição do Client.
    /// </summary>
    public string Description { get; set; } = null;

    /// <summary>
    /// Configuração do Client.
    /// </summary>
    public ClientConfigurationResponse ClientConfiguration { get; set; }

    /// <summary>
    /// Users vinculados ao Client.
    /// </summary>
    public ICollection<UserResponse> Users { get; set; }

    /// <summary>
    /// Users Admins vinculados ao Client.
    /// </summary>
    public ICollection<ClientUserAdminResponse> UserAdmins { get; set; }

    /// <summary> 
    /// Roles vinculadas ao Client.
    /// </summary>
    public ICollection<RoleEntity> Roles { get; set; }
}
