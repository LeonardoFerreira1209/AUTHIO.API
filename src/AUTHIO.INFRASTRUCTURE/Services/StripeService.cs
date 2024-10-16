using AUTHIO.DOMAIN.Contracts.Services.External;
using Microsoft.AspNetCore.Http;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviço de integração com o stripe.
/// </summary>
/// <param name="httpContextAccessor"></param>
public class StripeService(
    IHttpContextAccessor httpContextAccessor) : IStripeService
{
   
}
