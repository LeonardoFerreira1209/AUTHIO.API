using Stripe;

namespace AUTHIO.DOMAIN.Contracts.Services.External;

/// <summary>
/// Interface do serviço de integração do stripe.
/// </summary>
public interface IStripeService
{
    /// <summary>
    /// Retorna uma lista de produtos.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Product>> GetProductsAsync();
}
