using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Classe de extensão de assinatura.
/// </summary>
public static class SignatureExtensions
{
    /// <summary>
    /// Entity to response.
    /// </summary>
    /// <param name="signature"></param>
    /// <returns></returns>
    public static SignatureResponse ToResponse(this SignatureEntity signature, 
        bool includePlan = true, bool includeUser = true) => new()
        {
            Id = signature.Id,
            Created = signature.Created,
            Updated = signature.Updated,
            UserId = signature.UserId,
            PlanId = signature.PlanId,
            StartDateTime = signature.StartDateTime,
            EndDateTime = signature.EndDateTime,
            Status = signature.Status,
            User = includeUser 
                ? signature.User.ToResponse(false) 
                : null,
            Plan = includePlan
                ? signature.Plan.ToResponse(false)
                : null,
        };
}
