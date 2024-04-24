using Newtonsoft.Json;
using System.Net;

namespace AUTHIO.DOMAIN.Dtos.Response.Base;

/// <summary>
/// Retorno das APIS com erro.
/// </summary>
/// <remarks>
/// Response de error.
/// </remarks>
/// <param name="statusCode"></param>
/// <param name="notificacoes"></param>
public class ErrorResponse(
    HttpStatusCode statusCode,
    object dados, List<DadosNotificacao> notificacoes = null)
        : BaseApiResponse(statusCode, false, notificacoes)
{
    /// <summary>
    /// Dados a serem retornados na requisição.
    /// </summary>
    [JsonProperty(nameof(Dados))]
    public object Dados { get; } = dados;
}
