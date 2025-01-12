namespace AUTHIO.DOMAIN.Entities;

/// <summary>
/// Classe de vinculo entre usuário admin e Client.
/// </summary>
public class ClientIdentityUserAdminEntity : IEntityClient
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
    /// Id do Client responsavel.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Client.
    /// </summary>
    public virtual ClientEntity Client { get; private set; }
}
