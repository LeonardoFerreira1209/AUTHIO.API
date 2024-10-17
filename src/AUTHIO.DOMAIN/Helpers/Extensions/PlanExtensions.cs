using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;
using Stripe;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Classe de extensão de planos.
/// </summary>
public static class PlanExtensions
{
    /// <summary>
    /// Converte o produto do stripe para entidade de plano.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public static PlanEntity ToEntity(
        this Product product) => new()
        {
            Name = product.Name,
            Created = product.Created,
            Description = product.Description,
            ProductId = product.Id,
            MonthlyPayment = true,
            QuantTenants = Convert.ToInt32(product.Metadata.FirstOrDefault(x => x.Key == "QuantTenants").Value),
            QuantUsers = Convert.ToInt32(product.Metadata.FirstOrDefault(x => x.Key == "QuantUsers").Value),
            Value = ((product.DefaultPrice?.UnitAmountDecimal ?? 0) / 100),
            Status = Status.Ativo
        };

    /// <summary>
    /// Atualiza a entidade de planos com os dados do produto do stripe.
    /// </summary>
    /// <param name="planEntity"></param>
    /// <param name="product"></param>
    /// <returns></returns>
    public static PlanEntity ToEntityUpdate(
        this PlanEntity planEntity, 
        Product product)
    {
        planEntity.Name = product.Name;
        planEntity.Status = (Status)Convert.ToInt32(product.Active);
        planEntity.Description = product.Description;
        planEntity.Updated = DateTime.Now;
        planEntity.Value = ((product.DefaultPrice?.UnitAmountDecimal ?? 0) / 100);

        return planEntity;
    }

    /// <summary>
    /// Entity to response.
    /// </summary>
    /// <param name="plan"></param>
    /// <returns></returns>
    public static PlanResponse ToResponse(
        this PlanEntity plan, 
        bool includeSubscriptions = true) => new()
        {
            Created = plan.Created,
            Description = plan.Description,
            Id = plan.Id,
            MonthlyPayment = plan.MonthlyPayment,
            Name = plan.Name,
            QuantTenants = plan.QuantTenants,
            QuantUsers = plan.QuantUsers,
            Status = plan.Status,
            Updated = plan.Updated,
            Value = plan.Value,
            Subscriptions = includeSubscriptions
                ? plan.Subscriptions.Select(s => s.ToResponse(false, false)).ToList() 
                : null  
        };
}
