namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de user admin.
/// </summary>
public class ClientUserAdminResponse
{
    /// <summary>
    /// User Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Dados do usuário.
    /// </summary>
    public UserResponse User { get; set; }

    /// <summary>
    /// Id do Client responsavel.
    /// </summary>
    public Guid? ClientId { get; set; }

    /// <summary>
    /// Client.
    /// </summary>
    public ClientResponse Client { get; set; }
}
