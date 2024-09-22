using Newtonsoft.Json;
using System.Net;

namespace AUTHIO.DOMAIN.Dtos.Response.Base;

/// <summary>
/// Retorno das APIS com erro.
/// </summary>
/// <param name="statusCode"></param>
/// <param name="data"></param>
/// <param name="notificacoes"></param>
public class ErrorResponse(
    HttpStatusCode statusCode,
    object data, List<DataNotifications> notificacoes = null)
        : BaseApiResponse(statusCode, false, notificacoes)
{
    /// <summary>
    /// Dados a serem retornados na requisição.
    /// </summary>
    [JsonProperty(nameof(Data))]
    public object Data { get; } = data;
}
