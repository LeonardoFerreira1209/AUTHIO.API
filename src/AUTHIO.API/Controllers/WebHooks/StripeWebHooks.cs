using AUTHIO.DOMAIN.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using Stripe;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers.WebHooks;

/// <summary>
/// Controller que cuida do fluxo de webHooks do stripe.
/// </summary>
/// <param name="userService"></param>
[ApiController]
[Route("api/stripe/webhook")]
public class StripeWebHooks(
    IPlanService planService,
    IHttpContextAccessor httpContextAccessor) : ControllerBase()
{
    /// <summary>
    /// WebHook responsavel por atualizar dados de um produto com a base de dados.
    /// </summary>
    /// <returns></returns>
    [HttpPost("products")]
    [SwaggerOperation(Summary = "Eventos de produto do stripe.", Description = "Metodo responsavel por receber eventos de produtos do stripe!")]
    public async Task ProductEventsAsync(
        CancellationToken cancellationToken
        )
    {
        using (LogContext.PushProperty("WebHook", nameof(StripeWebHooks)))
        using (LogContext.PushProperty("Metodo", nameof(ProductEventsAsync)))
        {
            await CheckEventAsync(
                cancellationToken
            );
        }
    }

    private async Task CheckEventAsync(
        CancellationToken cancellationToken
        )
    {
        try
        {
            var json = await new StreamReader(
                httpContextAccessor.HttpContext.Request.Body)
                    .ReadToEndAsync(cancellationToken);

            var stripeEvent = EventUtility.ConstructEvent(
                json, Request.Headers["Stripe-Signature"],
                "whsec_4fbcdc5ac81bad044e740d917e7104d3b69f4d0973585136e3659646033f696f"
            );

            switch (stripeEvent.Type)
            {
                case EventTypes.ProductUpdated:
                    await planService.UpdateByProductAsync(
                        stripeEvent.Data.Object as Product,
                        cancellationToken
                    );
                    break;
                default: throw new NullReferenceException();
            }
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }
}
