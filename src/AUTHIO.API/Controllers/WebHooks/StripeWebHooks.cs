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
/// <param name="planService"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="configurations"></param>
[ApiController]
[Route("api/stripe/webhook")]
public class StripeWebHooks(
    IPlanService planService,
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configurations) : ControllerBase()
{
    /// <summary>
    /// assinatura do webHook stripe.
    /// </summary>
    private readonly string _signature = Environment.GetEnvironmentVariable("STRIPE_SIGNATURE")
                ?? configurations.GetSection("Stripe")["Signature"];

    /// <summary>
    /// WebHook responsavel por atualizar dados de um produto com a base de dados.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("products")]
    [SwaggerOperation(
        Summary = "Eventos de produto do stripe.", 
        Description = "Metodo responsavel por receber eventos de produtos do stripe!"
    )]
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

    /// <summary>
    /// Verifica qual tipo de evento de produto foi executado.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
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
                _signature
            );

            switch (stripeEvent.Type)
            {
                case EventTypes.ProductCreated:
                    await planService.CreateByProductAsync(
                       stripeEvent.Data.Object as Product,
                       cancellationToken
                    );
                    break;
                case EventTypes.ProductUpdated:
                    await planService.UpdateByProductAsync(
                        stripeEvent.Data.Object as Product,
                        cancellationToken
                    );
                    break;
                default: throw new NotSupportedException(
                    "Tipo de evento não encontrado!"
                );
            }
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }
}
