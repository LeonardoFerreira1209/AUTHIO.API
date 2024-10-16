using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.External;
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
    /// Endpoint responsável por fazer a autenticação do usuário, é retornado um token JWT (Json Web Token).
    /// </summary>
    /// <param name="authenticationRequest"></param>
    /// <returns></returns>
    [HttpPost("create-checkout-session")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Criação de sessão de checkout", Description = "Endpoint responsável por a criação da sessão de checkout do stripe.")]
    public async Task<IActionResult> CreateCheckoutSessionAsync(
        )
    {
        using (LogContext.PushProperty("Controller", nameof(StripeController)))
        using (LogContext.PushProperty("Metodo", nameof(CreateCheckoutSessionAsync)))
        {
            return await ExecuteAsync(nameof(CreateCheckoutSessionAsync),
                () => stripeService.CreateCheckoutSessionAsync(), "Criação de sessão de checkout");
        }
    }
}