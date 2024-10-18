using AUTHIO.DOMAIN.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Stripe;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers;

/// <summary>
/// Controller que cuida do fluxo de webHooks do stripe.
/// </summary>
/// <param name="userService"></param>
[ApiController]
[Route("api/stripe/webhooks/")]
public class StripeWebHooksController(
    IPlanService planService) : ControllerBase()
{
    /// <summary>
    /// WebHook responsavel por sincronizar dados de produtos com a base.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("products")]
    [EnableRateLimiting("fixed")]
    [SwaggerOperation(Summary = "Recebe dados de manipulação de produtos e sincronizar.", Description = "Metodo responsavel por receber dados do produto e sincronizar com a base!")]
    public async Task SyncProductsAsync(
        [FromBody] Product product,
        CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", "UserController"))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(product)))
        using (LogContext.PushProperty("Metodo", "RegisterAsync"))
        {
            Log.Information("Entrou no hoook");
        }
    }
}
