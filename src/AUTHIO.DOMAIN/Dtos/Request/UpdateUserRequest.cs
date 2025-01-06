using AUTHIO.DOMAIN.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Classe de atualização de usuários do sistema.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Id de referência.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Primeiro nome do usuário.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Ultimo nome do usuário.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Email do usuário.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Status do usuário.
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// Número do celular do usuário.
    /// </summary>
    public string PhoneNumber { get; set; }
}
