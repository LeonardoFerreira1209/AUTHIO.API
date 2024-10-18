using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de plano.
/// </summary>
public sealed class UpdatePlanRequest
{
    /// <summary>
    /// Id do plano.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Nomde do plano.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Descrição do plano.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Status.
    /// </summary>
    public Status Status { get; set; }

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
}
