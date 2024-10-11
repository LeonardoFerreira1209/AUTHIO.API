using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Classe de extensão de planos.
/// </summary>
public static class PlanExtensions
{
    /// <summary>
    /// Entity to response.
    /// </summary>
    /// <param name="plan"></param>
    /// <returns></returns>
    public static PlanResponse ToResponse(
        this PlanEntity plan, 
        bool includeUsers = true) => new()
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
            Users = includeUsers 
                ? plan.Users.Select(u => u.ToResponse()).ToList() 
                : null  
        };
}
