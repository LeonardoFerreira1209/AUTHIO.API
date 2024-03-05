﻿using AUTHIO.APPLICATION.Infra.Context;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// Classe de vinculo entre usuário admin e tenant.
/// </summary>
public class TenantUserAdminEntity
{
    /// <summary>
    /// User Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Dados do usuário.
    /// </summary>
    public virtual UserEntity User { get; private set; }

    /// <summary>
    /// Id do tenant responsavel.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Tenant.
    /// </summary>
    public virtual TenantEntity Tenant { get; private set; }
}
