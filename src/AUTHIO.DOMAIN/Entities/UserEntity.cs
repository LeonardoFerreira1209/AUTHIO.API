using AUTHIO.DOMAIN.Enums;
using Microsoft.AspNetCore.Identity;

namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de entidade de usuário.
/// </summary>
public class UserEntity
    : IdentityUser<Guid>, IEntityBase, IEntityTenantNullAble, IEntitySystem
{
    /// <summary>
    /// ctor
    /// </summary>
    public UserEntity()
    {

    }

    /// <summary>
    /// ctor
    /// </summary>
    public UserEntity(string firstName, string lastName,
        string userName, string email, string phoneNumber, Status status,
        DateTime created, bool emailConfirmed,
        Guid planId, DateTime? updated = null,
        Guid? tenantId = null, bool system = false)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        EmailConfirmed = emailConfirmed;
        PhoneNumber = phoneNumber;
        Status = status;
        Created = created;
        Updated = updated;
        PlanId = planId;
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
    /// Id do plano vinculado ao usuário.
    /// </summary>
    public Guid PlanId { get; set; }

    /// <summary>
    /// Dados do plano.
    /// </summary>
    public virtual PlanEntity Plan { get; private set; }

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
    public bool System { get; private set; }
}
