using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Classe de extensão de assinatura.
/// </summary>
public static class SubscriptionExtensions
{
    /// <summary>
    /// Entity to response.
    /// </summary>
    /// <param name="Subscription"></param>
    /// <returns></returns>
    public static SubscriptionResponse ToResponse(this SubscriptionEntity Subscription, 
        bool includePlan = true, bool includeUser = true) => new()
        {
            Id = Subscription.Id,
            Created = Subscription.Created,
            Updated = Subscription.Updated,
            UserId = Subscription.UserId,
            PlanId = Subscription.PlanId,
            StartDateTime = Subscription.StartDateTime,
            EndDateTime = Subscription.EndDateTime,
            Status = Subscription.Status,
            User = includeUser 
                ? Subscription.User.ToResponse(false) 
                : null,
            Plan = includePlan
                ? Subscription.Plan.ToResponse(false)
                : null,
        };
}
