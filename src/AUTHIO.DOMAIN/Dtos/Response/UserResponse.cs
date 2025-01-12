using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de Usúario.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// E-mail
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Id do Client responsavel.
    /// </summary>
    public Guid? ClientId { get; set; }

    /// <summary>
    /// Client.
    /// </summary>
    public ClientResponse Client { get; set; }

    /// <summary>
    /// Id da assinatura.
    /// </summary>
    public Guid? SubscriptionId { get; set; }

    /// <summary>
    /// Dados da assinatura.
    /// </summary>
    public SubscriptionResponse Subscription { get; set; } 

    /// <summary>
    /// Nome do usuário.
    /// </summary>
    public string Name { get; set; } = null;

    /// <summary>
    /// Ultimo nome do usuário.
    /// </summary>
    public string LastName { get; set; } = null;

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
    /// Usuário do sistema.
    /// </summary>
    public bool System { get; set; }
}
