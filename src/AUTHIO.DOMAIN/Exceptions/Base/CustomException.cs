using AUTHIO.DOMAIN.Dtos.Response.Base;
using System.Net;

namespace AUTHIO.DOMAIN.Exceptions.Base;

/// <summary>
/// Exception customizado.
/// </summary>
public class CustomException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public CustomException(
        HttpStatusCode statusCode,
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
            (statusCode, dados, notificacoes);
    }
}
