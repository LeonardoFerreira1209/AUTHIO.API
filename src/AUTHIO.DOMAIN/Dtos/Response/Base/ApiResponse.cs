using Newtonsoft.Json;
using System.Net;

namespace AUTHIO.DOMAIN.Dtos.Response.Base;

/// <summary>
/// Retorno das APIS.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T>
    : BaseApiResponse where T : class
{
    /// <summary>
    /// Construtor simples
    /// </summary>
    /// <param name="statusCode"></param>
    public ApiResponse(HttpStatusCode statusCode) : base(statusCode) { }

    /// <summary>
    /// Construtor sem recebimento de dados.
    /// </summary>
    /// <param name="success"></param>
    /// <param name="statusCode"></param>
    /// <param name="notifications"></param>
    public ApiResponse(bool success, HttpStatusCode statusCode,
        List<DataNotifications> notifications = null)
            : base(statusCode, success, notifications)
    {

    }

    /// <summary>
    /// Construtor que recebe todos os itens.
    /// </summary>
    /// <param name="success"></param>
    /// <param name="statusCode"></param>
    /// <param name="data"></param>
    /// <param name="notifications"></param>
    public ApiResponse(bool success, HttpStatusCode statusCode,
        T data = null, List<DataNotifications> notifications = null)
            : base(statusCode, success, notifications)
    {
        Data = data;
    }

    /// <summary>
    /// Dados a serem retornados na requisição.
    /// </summary>
    [JsonProperty(nameof(Data))]
    public T Data { get; }
}
