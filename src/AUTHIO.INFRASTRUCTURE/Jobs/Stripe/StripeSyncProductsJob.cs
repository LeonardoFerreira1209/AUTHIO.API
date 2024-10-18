using AUTHIO.DOMAIN.Contracts.Jobs;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;
using AUTHIO.INFRASTRUCTURE.Services;
using Newtonsoft.Json;
using Serilog;
using Stripe;

namespace AUTHIO.INFRASTRUCTURE.Jobs.Stripe;

/// <summary>
/// Job de sincronização de produtos do Stripe com a base.
/// </summary>
public sealed class StripeSyncProductsJob(
    StripeService stripeService,
    PlanRepository planRepository,
    UnitOfWork<AuthIoContext> unitOfWork) : IStripeSyncProductsJob
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
    private async Task SyncPlanStripesAsync()
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

                        }).Unwrap();
                    }

                    await unitOfWork
                        .CommitAsync();

                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }
}
