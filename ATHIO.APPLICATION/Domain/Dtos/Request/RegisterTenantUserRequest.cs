﻿namespace AUTHIO.APPLICATION.Domain.Dtos.Request;

/// <summary>
/// Request de criação de usuário do Tenant.
/// </summary>
public sealed class RegisterTenantUserRequest
{
    /// <summary>
    /// Primeiro nome do usuário.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Ultimo nome do usuário.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Nome de usuário (Login).
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Senha do usuário.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Email do usuário.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Número do celular do usuário.
    /// </summary>
    public string PhoneNumber { get; set; }
}
