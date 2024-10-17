using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de plano.
/// </summary>
public class PlanResponse
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
    /// Nome do plano.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descrição do plano.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Valor do plano.
    /// </summary>
    public decimal Value { get; set; }

    /// <summary>
    /// Pagamento mensal.
    /// </summary>
    public bool MonthlyPayment { get; set; }

    /// <summary>
    /// Quantidade de tenants liberado para cadastro.
    /// </summary>
    public int QuantTenants { get; set; }

    /// <summary>
    /// Quantidade de usuários liberado para cadastro em cada tenant.
    /// </summary>
    public int QuantUsers { get; set; }

    /// <summary>
    /// Coleção de assinaturas vinculados ao plano.
    /// </summary>
    public virtual ICollection<SubscriptionResponse> Subscriptions { get; set; }
}
