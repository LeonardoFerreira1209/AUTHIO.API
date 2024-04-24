using Newtonsoft.Json;
using System.Net;

namespace AUTHIO.DOMAIN.Dtos.Response.Base;

/// <summary>
/// Dados a ser retornado em uma notificação do sistema.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="mensagem"></param>
public class DadosNotificacao(string mensagem)
{
    /// <summary>
    /// Mensagem da notificação.
    /// </summary>
    [JsonProperty(nameof(Mensagem))]
    public string Mensagem { get; } = mensagem;
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
    /// ctor recebendo status, bool de sucesso, e lista de notificações. 
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="sucesso"></param>
    /// <param name="notificacoes"></param>
    public BaseApiResponse(HttpStatusCode statusCode,
        bool sucesso, List<DadosNotificacao> notificacoes)
    {
        StatusCode = statusCode;
        Sucesso = sucesso;
        Notificacoes = notificacoes;
    }

    /// <summary>
    /// Status code.
    /// </summary>
    [JsonProperty(nameof(StatusCode))]
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Retorna true se a requisição para API foi bem sucedida.
    /// </summary>
    [JsonProperty(nameof(Sucesso))]
    public bool Sucesso { get; }

    /// <summary>
    /// Notificações que retornam da requisição, sejam elas Sucesso, Erro, Informação.
    /// </summary>
    [JsonProperty(nameof(Notificacoes))]
    public List<DadosNotificacao> Notificacoes { get; }
}
