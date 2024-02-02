using System.Net;

namespace AUTHIO.APPLICATION.Domain.Dtos.Response.Base;

/// <summary>
/// Retorno das APIS com erro.
/// </summary>
public class ErrorResponse : BaseApiResponse
{
    public ErrorResponse(HttpStatusCode statusCode, List<DadosNotificacao> notificacoes = null) : base(statusCode, false, notificacoes) { }

    public ErrorResponse(HttpStatusCode statusCode, object dados = null, List<DadosNotificacao> notificacoes = null) : base(statusCode, false, dados, notificacoes) { }
}
