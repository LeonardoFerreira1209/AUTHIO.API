using AUTHIO.DOMAIN.Contracts.Services.External;
using Newtonsoft.Json;
using Serilog;
using Stripe;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviço de integração com o stripe.
/// </summary>
/// <param name="httpContextAccessor"></param>
public class StripeService() : IStripeService
{
    /// <summary>
    /// Serviçp de preços do stripe.
    /// </summary>
    private readonly PriceService _priceService = new();

    /// <summary>
    /// Serviço de produtos do stripe.
    /// </summary>
    private readonly ProductService _productService = new();

    /// <summary>
    ///  Recupera uma lista de produtos.
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        Log.Information(
          $"[LOG INFORMATION] - SET TITLE {nameof(StripeService)} - METHOD {nameof(GetProductsAsync)}\n");

        try
        {
            return await _productService
                .ListAsync(new ProductListOptions
                {
                    Active = true,

                }).ContinueWith(async (productsResult) =>
                {
                    var products = productsResult.Result;

                    foreach(var product in products.Data)
                    {
                        var price = await _priceService.GetAsync(
                            product.DefaultPriceId
                        );

                        product.DefaultPrice = price;
                    }

                    return products
                        .OrderBy(p => 
                            (p.DefaultPrice.UnitAmountDecimal)
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
