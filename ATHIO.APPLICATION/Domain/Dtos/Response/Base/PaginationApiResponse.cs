using Newtonsoft.Json;
using System.Net;

namespace AUTHIO.APPLICATION.Domain.Dtos.Response.Base;

/// <summary>
/// Retorno das APIS.
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
            : base(statusCode, sucesso, notificacoes) {
    }

    /// <summary>
    /// Construtor que recebe todos os itens.
    /// </summary>
    /// <param name="sucesso"></param>
    /// <param name="statusCode"></param>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public PaginationApiResponse(bool sucesso, HttpStatusCode statusCode, 
        List<T> dados, int totalDados, int totalPaginas, 
        int paginaAtual, int tamanhoPagina, List<DadosNotificacao> notificacoes = null)
            : base(statusCode, sucesso, notificacoes)  
    {
        Dados = dados;
        TotalDados = totalDados;
        PaginaAtual = paginaAtual;
        TotalPaginas = totalPaginas;
        TamanhoPagina = tamanhoPagina;
    }

    /// <summary>
    /// Tamanho da pagina.
    /// </summary>
    [JsonProperty(nameof(TamanhoPagina))]
    public int TamanhoPagina { get; }

    /// <summary>
    /// Pagina atual.
    /// </summary>
    [JsonProperty(nameof(PaginaAtual))]
    public int PaginaAtual { get; }

    /// <summary>
    /// Total de dados na pagina.
    /// </summary>
    [JsonProperty(nameof(TotalPaginas))]
    public int TotalPaginas { get; }

    /// <summary>
    /// Total de dados na base.
    /// </summary>
    [JsonProperty(nameof(TotalDados))]
    public int TotalDados { get; }

    /// <summary>
    /// Dados a serem retornados na requisição.
    /// </summary>
    [JsonProperty(nameof(Dados))]
    public List<T> Dados { get; }
}
