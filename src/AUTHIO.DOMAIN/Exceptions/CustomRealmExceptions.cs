using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Exceptions.Base;
using System.Net;

namespace AUTHIO.DOMAIN.Exceptions;

/// <summary>
/// Classe de exceptions de Realms.
/// </summary>
public sealed class CustomRealmExceptions
{
    /// <summary>
    /// Exception para Realm não encontrado.
    /// </summary>
    public sealed class NotFoundRealmException : BaseException
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        public NotFoundRealmException(
            object dados = null)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, [
                   new("Realm não encontrado na base de dados!")
               ]);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="notificacoes"></param>
        public NotFoundRealmException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, notificacoes);
        }
    }
}

/// <summary>
/// Exception para usuarios sem permissões a um Realm.
/// </summary>
public sealed class NotPermissionRealmException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public NotPermissionRealmException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Unauthorized, dados, [
               new("Realm não pose ser manipulado por esse usuário!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public NotPermissionRealmException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.NotFound, dados, notificacoes);
    }
}

/// <summary>
/// Exception para Realm já cadastrado.
/// </summary>
public sealed class DuplicatedRealmException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public DuplicatedRealmException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Ambiguous, dados, [
               new("Realm com o mesmo nome já cadastrado!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public DuplicatedRealmException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Ambiguous, dados, notificacoes);
    }
}

/// <summary>
/// Exception para Realm com falha na criação.
/// </summary>
public sealed class CreateRealmFailedException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public CreateRealmFailedException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.BadRequest, dados, [
               new("Falha na criação do Realm!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public CreateRealmFailedException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.BadRequest, dados, notificacoes);
    }
}
