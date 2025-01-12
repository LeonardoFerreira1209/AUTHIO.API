using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Exceptions.Base;
using System.Net;

namespace AUTHIO.DOMAIN.Exceptions;

/// <summary>
/// Classe de exceptions de Clients.
/// </summary>
public sealed class CustomClientExceptions
{
    /// <summary>
    /// Exception para Client não encontrado.
    /// </summary>
    public sealed class NotFoundClientException : BaseException
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        public NotFoundClientException(
            object dados = null)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, [
                   new("Client não encontrado na base de dados!")
               ]);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="notificacoes"></param>
        public NotFoundClientException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, notificacoes);
        }
    }
}

/// <summary>
/// Exception para usuarios sem permissões a um Client.
/// </summary>
public sealed class NotPermissionClientException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public NotPermissionClientException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Unauthorized, dados, [
               new("Client não pose ser manipulado por esse usuário!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public NotPermissionClientException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.NotFound, dados, notificacoes);
    }
}

/// <summary>
/// Exception para Client já cadastrado.
/// </summary>
public sealed class DuplicatedClientException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public DuplicatedClientException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Ambiguous, dados, [
               new("Client com o mesmo nome já cadastrado!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public DuplicatedClientException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Ambiguous, dados, notificacoes);
    }
}

/// <summary>
/// Exception para Client com falha na criação.
/// </summary>
public sealed class CreateClientFailedException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public CreateClientFailedException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.BadRequest, dados, [
               new("Falha na criação do Client!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public CreateClientFailedException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.BadRequest, dados, notificacoes);
    }
}
