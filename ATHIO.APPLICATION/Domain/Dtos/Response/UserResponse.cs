using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Dtos.Response;

/// <summary>
/// Classe de response de Usúario.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id do tenant responsavel.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// E-mail
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Tenant.
    /// </summary>
    public TenantResponse Tenant { get; set; }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Ultimo nome do usuário.
    /// </summary>
    public string LastName { get; set; } = null;

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; } = null;

    /// <summary>
    /// Status do cadastro.
    /// </summary>
    public Status Status { get; set; }
}
