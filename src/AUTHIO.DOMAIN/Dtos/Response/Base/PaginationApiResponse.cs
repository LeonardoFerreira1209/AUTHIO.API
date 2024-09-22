using System.Net;

namespace AUTHIO.DOMAIN.Dtos.Response.Base;

/// <summary>
/// Retorno das APIS paginada.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginationApiResponse<T>
    : BaseApiResponse where T : class
{
    /// <summary>
    /// Construtor simples
    /// </summary>
    /// <param name="statusCode"></param>
    public PaginationApiResponse(HttpStatusCode statusCode)
        : base(statusCode) { }

    /// <summary>
    /// Construtor com recebimento de dados parcial.
    /// </summary>
    /// <param name="success"></param>
    /// <param name="statusCode"></param>
    /// <param name="notifications"></param>
    public PaginationApiResponse(bool success, HttpStatusCode statusCode,
        List<DataNotifications> notifications = null)
            : base(statusCode, success, notifications)
    {
    }

    /// <summary>
    /// Construtor que recebe todos os itens.
    /// </summary>
    /// <param name="success"></param>
    /// <param name="statusCode"></param>
    /// <param name="paginatedResponse"></param>
    /// <param name="notifications"></param>
    public PaginationApiResponse(bool success, HttpStatusCode statusCode,
        PaginatedResponse<T> paginatedResponse, List<DataNotifications> notifications = null)
            : base(statusCode, success, notifications)
    {
        Pagination = paginatedResponse;
    }

    /// <summary>
    /// Dados da paginação.
    /// </summary>
    public PaginatedResponse<T> Pagination { get; set; }
}
