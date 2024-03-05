using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.DOMAIN.ENTITY;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace AUTHIO.APPLICATION.Domain.Entity;

/// <summary>
/// Classe de entidade de roles.
/// </summary>
public class RoleEntity : IdentityRole<Guid>,
    IEntityBase, IEntityTenantNullAble, IEntitySystem, IFilterableEntity<RoleEntity>
{
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

    /// <summary>
    /// Id do tenant.
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// Tenant.
    /// </summary>
    public virtual TenantEntity Tenant { get; private set; }

    /// <summary>
    /// Role do sistema.
    /// </summary>
    public bool System { get; set; }

    /// <summary>
    /// Filtragem global da entidade.
    /// </summary>
    /// <param name="contextService"></param>
    /// <returns></returns>
    public Expression<Func<RoleEntity, bool>> GetFilterExpression(AuthIoContext authIoContext)
        => entidade => (entidade.TenantId 
                == authIoContext._tenantId && !entidade.System) 
                    || (entidade.TenantId == null && entidade.System);
}
