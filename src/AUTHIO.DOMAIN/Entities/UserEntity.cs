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
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="status"></param>
    /// <param name="created"></param>
    /// <param name="emailConfirmed"></param>
    /// <param name="signatureId"></param>
    /// <param name="updated"></param>
    /// <param name="tenantId"></param>
    /// <param name="system"></param>
    public UserEntity(string firstName, string lastName,
        string userName, string email, string phoneNumber, Status status,
        DateTime created, bool emailConfirmed,
        Guid? signatureId, DateTime? updated = null,
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
        SignatureId = signatureId;
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
    /// Id da assinatura vinculado ao usuário.
    /// </summary>
    public Guid? SignatureId { get; set; }

    /// <summary>
    /// Dados da assinatura.
    /// </summary>
    public virtual SignatureEntity Signature { get; private set; }

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
