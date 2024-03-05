﻿using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// Classe de entidade de tenant.
/// </summary>
public class TenantEntity : IEntityBase
{
    /// <summary>
    /// ctor
    /// </summary>
    public TenantEntity() {
        Users = [];
        Roles = [];
    }

    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get ; set; }

    /// <summary>
    /// Usuário de criação.
    /// </summary>
    public Guid UserId { get; set; }

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
    public virtual ICollection<UserEntity> Users { get; private set; }
    
    /// <summary>
    /// Users Admins vinculados ao tenant.
    /// </summary>
    public virtual ICollection<TenantUserAdminEntity> UserAdmins { get; private set; }

    /// <summary> 
    /// Roles vinculadas ao tenant.
    /// </summary>
    public virtual ICollection<RoleEntity> Roles { get; private set; }
}
