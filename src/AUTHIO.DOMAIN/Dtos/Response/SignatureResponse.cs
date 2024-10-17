using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de assinatura.
/// </summary>
public class SubscriptionResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

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
    /// Id do plano.
    /// </summary>
    public Guid PlanId { get; set; }

    /// <summary>
    /// Dados do plano.
    /// </summary>
    public PlanResponse Plan { get; set; }

    /// <summary>
    /// Id do usuário.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Dados do usuário.
    /// </summary>
    public UserResponse User { get; set; }

    /// <summary>
    /// Data de inicio da vigência.
    /// </summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>
    /// Data de fim da vigência.
    /// </summary>
    public DateTime EndDateTime { get; set; }
}
