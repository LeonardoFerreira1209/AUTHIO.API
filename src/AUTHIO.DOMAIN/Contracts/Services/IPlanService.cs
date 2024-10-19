using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Interface de planos.
/// </summary>
public interface IPlanService
{
    /// <summary>
    /// Cria um novo plano baseado em um produto do stripe.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> CreateByProductAsync(
        Product product,
        CancellationToken cancellationToken
     );

    /// <summary>
    /// Atualiza o plano através do id de um produto do stripe.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> UpdateByProductAsync(
        Product product,
        CancellationToken cancellationToken
     );
}
