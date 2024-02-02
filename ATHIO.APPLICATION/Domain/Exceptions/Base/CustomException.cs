using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using System.Net;

namespace AUTHIO.APPLICATION.Domain.Exceptions.Base;

/// <summary>
/// Exception customizado.
/// </summary>
public class CustomException : BaseException
{
    public CustomException(HttpStatusCode statusCode, object dados, List<DadosNotificacao> notificacoes)
    {
        Response = new ErrorResponse
            (statusCode, dados, notificacoes);
    }
}
