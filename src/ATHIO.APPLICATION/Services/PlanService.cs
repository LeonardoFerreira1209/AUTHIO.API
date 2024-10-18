using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.External;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.INFRASTRUCTURE.Context;
using Newtonsoft.Json;
using Serilog;

namespace AUTHIO.APPLICATION.Services;

/// <summary>
/// Serviço de planos.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public sealed class PlanService(
        IUnitOfWork<AuthIoContext> unitOfWork,
        IPlanRepository planRepository,
        IStripeService stripeService) : IPlanService
{
    /// <summary>
    /// Executa uma task.
    /// </summary>
    /// <returns></returns>
    public async Task ExecuteAsync()
        => await SyncPlanStripesAsync();

    /// <summary>
    /// Sincroniza a tabela de planos com os produtos do stripe.
    /// </summary>
    /// <returns></returns>
    public async Task SyncPlanStripesAsync()
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(PlanService)} - METHOD {nameof(SyncPlanStripesAsync)}\n");

        try
        {
            await stripeService.GetProductsAsync()
                .ContinueWith(async (productsTask) =>
                {
                    var products 
                        = productsTask.Result;

                    foreach (var product in products)
                    {
                        await planRepository.GetAsync(
                            x => x.ProductId.Equals(product.Id)

                        ).ContinueWith(async (planTask) =>
                        {
                            var plan = planTask.Result;

                            if (plan is null)
                                await planRepository.CreateAsync(
                                    product.ToEntity()
                                );
                            else
                                await planRepository.UpdateAsync(
                                    plan.ToEntityUpdate(product)
                                );

                            await unitOfWork.CommitAsync();

                        }).Unwrap();
                    }
                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }
}
