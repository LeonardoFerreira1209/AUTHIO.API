using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Contracts.Services.External;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers.Stripe;

/// <summary>
/// Controller de integração com stripe.
/// </summary>
/// <param name="featureFlags"></param>
/// <param name="stripeService"></param>
[ApiController]
[Route("api/stripe")]
public class StripeController(
    IFeatureFlagsService featureFlags, IStripeService stripeService)
     : BaseController(featureFlags)
{
    /// <summary>
    /// Endpoint responsavel por trazer os produtos do Stripe.
    /// </summary>
    /// <returns></returns>
    [HttpGet("get-all-plans")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Recupera os produtos no stripe", Description = "Endpoint responsável por recupera os produtos no stripe.")]
    public async Task<Object> GetProductsAsync(
        )
    {
        using (LogContext.PushProperty("Controller", nameof(StripeController)))
        using (LogContext.PushProperty("Metodo", nameof(GetProductsAsync)))
        {
            return await ExecuteAsync(nameof(GetProductsAsync),
                () => stripeService.GetProductsAsync(), "Recupera os produtos no stripe");
        }
    }
}