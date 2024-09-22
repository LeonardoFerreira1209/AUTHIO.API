using Newtonsoft.Json;
using System.Net;

namespace AUTHIO.DOMAIN.Dtos.Response.Base;

/// <summary>
/// Dados a ser retornado em uma notificação do sistema.
/// </summary>
/// <param name="message"></param>
public class DataNotifications(string message)
{
    /// <summary>
    /// Mensagem da notificação.
    /// </summary>
    [JsonProperty(nameof(Message))]
    public string Message { get; } = message;
}

/// <summary>
/// Classe 
/// </summary>
public abstract class BaseApiResponse
{
    /// <summary>
    /// ctor recebendo o status.
    /// </summary>
    /// <param name="statusCode"></param>
    public BaseApiResponse(
        HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// recebendo status, bool de sucesso, e lista de notificações. 
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="success"></param>
    /// <param name="notifications"></param>
    public BaseApiResponse(HttpStatusCode statusCode,
        bool success, List<DataNotifications> notifications)
    {
        StatusCode = statusCode;
        Success = success;
        Notifications = notifications;
    }

    /// <summary>
    /// Status code.
    /// </summary>
    [JsonProperty(nameof(StatusCode))]
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Retorna true se a requisição para API foi bem sucedida.
    /// </summary>
    [JsonProperty(nameof(Success))]
    public bool Success { get; }

    /// <summary>
    /// Notificações que retornam da requisição, sejam elas Sucesso, Erro, Informação.
    /// </summary>
    [JsonProperty(nameof(Notifications))]
    public List<DataNotifications> Notifications { get; }
}
