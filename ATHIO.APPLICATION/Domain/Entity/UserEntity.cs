﻿using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.DOMAIN.ENTITY;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// Classe de entidade de usuário.
/// </summary>
public class UserEntity 
    : IdentityUser<Guid>, IEntityBase, IEntityTenantNullAble, IEntitySystem, IFilterableEntity<UserEntity>
{
    /// <summary>
    /// ctor
    /// </summary>
    public UserEntity() {

    }

    /// <summary>
    /// ctor
    /// </summary>
    public UserEntity(string firstName, string lastName, 
        string userName, string email, string phoneNumber, Status status,
        DateTime created, bool emailConfirmed, 
        DateTime? updated = null, Guid? tenantId = null, bool system = false)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        Status = status;
        Created = created;
        Updated = updated;
        EmailConfirmed = emailConfirmed;
        TenantId = tenantId;
        System = system;
    }

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Ultimo nome do usuário.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Id do tenant responsavel.
    /// </summary>
    public Guid? TenantId { get; private set; }

    /// <summary>
    /// Tenant.
    /// </summary>
    public virtual TenantEntity Tenant { get; private set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; private set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; private set; }

    /// <summary>
    /// Status do cadastro.
    /// </summary>
    public Status Status { get; private set; }

    /// <summary>
    /// Pertence ao sistema.
    /// </summary>
    public bool System {get; private set; }

    /// <summary>
    /// Filtragem global da entidade.
    /// </summary>
    /// <param name="contextService"></param>
    /// <returns></returns>
    public Expression<Func<UserEntity, bool>> GetFilterExpression([FromServices] AuthIoContext authIoContext)
         => entidade => (entidade.TenantId == authIoContext._tenantId && !entidade.System)
                    || (entidade.TenantId == null && entidade.System);
}
