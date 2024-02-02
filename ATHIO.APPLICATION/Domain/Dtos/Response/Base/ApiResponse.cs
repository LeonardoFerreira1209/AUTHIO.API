using System.Net;

namespace AUTHIO.APPLICATION.Domain.Dtos.Response.Base;

/// <summary>
/// Retorno das APIS.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ApiResponse<T> : BaseApiResponse
{
    /// <summary>
    /// Construtor simples
    /// </summary>
    /// <param name="statusCode"></param>
    public ApiResponse(HttpStatusCode statusCode) : base(statusCode) { }

    /// <summary>
    /// Construtor sem recebimento de dados.
    /// </summary>
    /// <param name="sucesso"></param>
    /// <param name="statusCode"></param>
    /// <param name="notificacoes"></param>
    public ApiResponse(bool sucesso, HttpStatusCode statusCode, List<DadosNotificacao> notificacoes = null) : base(statusCode, sucesso, notificacoes) { }

    /// <summary>
    /// Construtor que recebe todos os itens.
    /// </summary>
    /// <param name="sucesso"></param>
    /// <param name="statusCode"></param>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public ApiResponse(bool sucesso, HttpStatusCode statusCode, object dados = null, List<DadosNotificacao> notificacoes = null) : base(statusCode, sucesso, dados, notificacoes) { }
}
