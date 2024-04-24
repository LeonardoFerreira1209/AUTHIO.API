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
    /// Construtor sem recebimento de dados.
    /// </summary>
    /// <param name="sucesso"></param>
    /// <param name="statusCode"></param>
    /// <param name="notificacoes"></param>
    public PaginationApiResponse(bool sucesso, HttpStatusCode statusCode,
        List<DadosNotificacao> notificacoes = null)
            : base(statusCode, sucesso, notificacoes)
    {
    }

    /// <summary>
    /// Construtor que recebe todos os itens.
    /// </summary>
    /// <param name="sucesso"></param>
    /// <param name="statusCode"></param>
    /// <param name="paginatedResponse"></param>
    /// <param name="notificacoes"></param>
    public PaginationApiResponse(bool sucesso, HttpStatusCode statusCode,
        PaginatedResponse<T> paginatedResponse, List<DadosNotificacao> notificacoes = null)
            : base(statusCode, sucesso, notificacoes)
    {
        Paginacao = paginatedResponse;
    }

    /// <summary>
    /// Dados da paginação.
    /// </summary>
    public PaginatedResponse<T> Paginacao { get; set; }
}
