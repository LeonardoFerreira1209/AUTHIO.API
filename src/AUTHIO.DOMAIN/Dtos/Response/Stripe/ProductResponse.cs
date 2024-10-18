using Newtonsoft.Json;

namespace AUTHIO.DOMAIN.Dtos.Response.Stripe;

/// <summary>
/// Response de produto do Stripe.
/// </summary>
public class ProductResponse
{
    /// <summary>
    /// Identificador único do produto.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Tipo do objeto retornado.
    /// </summary>
    [JsonProperty("object")]
    public string Object { get; set; }

    /// <summary>
    /// Indica se o produto está ativo.
    /// </summary>
    [JsonProperty("active")]
    public bool Active { get; set; }

    /// <summary>
    /// Data de criação do produto (timestamp).
    /// </summary>
    [JsonProperty("created")]
    public int Created { get; set; }

    /// <summary>
    /// Preço padrão do produto.
    /// </summary>
    [JsonProperty("default_price")]
    public object DefaultPrice { get; set; }

    /// <summary>
    /// Descrição do produto.
    /// </summary>
    [JsonProperty("description")]
    public object Description { get; set; }

    /// <summary>
    /// Lista de URLs das imagens do produto.
    /// </summary>
    [JsonProperty("images")]
    public List<object> Images { get; set; }

    /// <summary>
    /// Lista de características de marketing do produto.
    /// </summary>
    [JsonProperty("marketing_features")]
    public List<object> MarketingFeatures { get; set; }

    /// <summary>
    /// Indica se o produto está em modo ao vivo (produção).
    /// </summary>
    [JsonProperty("livemode")]
    public bool Livemode { get; set; }

    /// <summary>
    /// Metadados adicionais do produto.
    /// </summary>
    [JsonProperty("metadata")]
    public IDictionary<string, object> Metadata { get; set; }

    /// <summary>
    /// Nome do produto.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Dimensões do pacote do produto, se aplicável.
    /// </summary>
    [JsonProperty("package_dimensions")]
    public object PackageDimensions { get; set; }

    /// <summary>
    /// Indica se o produto é enviado (shippable).
    /// </summary>
    [JsonProperty("shippable")]
    public object Shippable { get; set; }

    /// <summary>
    /// Descriptor da fatura relacionado ao produto.
    /// </summary>
    [JsonProperty("statement_descriptor")]
    public object StatementDescriptor { get; set; }

    /// <summary>
    /// Código fiscal do produto.
    /// </summary>
    [JsonProperty("tax_code")]
    public object TaxCode { get; set; }

    /// <summary>
    /// Rótulo da unidade do produto.
    /// </summary>
    [JsonProperty("unit_label")]
    public object UnitLabel { get; set; }

    /// <summary>
    /// Data da última atualização do produto (timestamp).
    /// </summary>
    [JsonProperty("updated")]
    public int Updated { get; set; }

    /// <summary>
    /// URL relacionada ao produto.
    /// </summary>
    [JsonProperty("url")]
    public object Url { get; set; }
}
