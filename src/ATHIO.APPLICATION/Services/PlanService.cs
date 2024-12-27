using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.External;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.INFRASTRUCTURE.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Stripe;
using System.Net;

namespace AUTHIO.APPLICATION.Services;

/// <summary>
/// Serviço de planos.
/// </summary>
/// <param name="unitOfWork"></param>
/// <param name="planRepository"></param>
/// <param name="stripeService"></param>
public sealed class PlanService(
        IUnitOfWork<AuthIoContext> unitOfWork,
        IPlanRepository planRepository,
        IStripeService stripeService) : IPlanService
{
    /// <summary>
    /// Cria um novo plano baseado em um produto do stripe.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ObjectResult> CreateByProductAsync(
       Product product,
       CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(PlanService)} - METHOD {nameof(CreateByProductAsync)}\n");

        try
        {
            var plan = await planRepository
                .CreateAsync(
                    product.ToEntity()
                );

            await unitOfWork.CommitAsync();

            return new ObjectResponse(
                HttpStatusCode.Created,
                new ApiResponse<PlanResponse>(
                    true,
                    HttpStatusCode.Created,
                    plan.ToResponse(), [
                        new DataNotifications(
                            "plano criado com sucesso!"
                        )
                    ]
                )
            );
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }

    /// <summary>
    /// Método responsável por atualizar um plano atraves de um produto Id.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ObjectResult> UpdateByProductAsync(
       Product product,
       CancellationToken cancellationToken
    )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(PlanService)} - METHOD {nameof(UpdateByProductAsync)}\n");

        try
        {
            return await planRepository
                .GetAsync(p => p.ProductId.Equals(product.Id)).ContinueWith(async (planEntityTask) =>
                {
                    var plan
                        = planEntityTask.Result
                        ?? throw new Exception("Plano não encontrado!");

                    var price = await stripeService.GetPriceByIdAsync(
                        product.DefaultPriceId
                    );

                    product.DefaultPrice = price;

                    await planRepository.UpdateAsync(
                        plan.ToEntityUpdate(
                            product
                        )
                    );

                    await unitOfWork.CommitAsync();

                    return new ObjectResponse(
                        HttpStatusCode.OK,
                        new ApiResponse<PlanResponse>(
                            true,
                            HttpStatusCode.OK,
                            plan.ToResponse(), [
                                new DataNotifications("plano atualizado com sucesso!")
                            ]
                        )
                    );

                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

            throw;
        }
    }
}
