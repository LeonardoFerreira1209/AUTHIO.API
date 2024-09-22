using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Exceptions.Base;
using System.Net;

namespace AUTHIO.DOMAIN.Exceptions;

/// <summary>
/// Classe de exceptions de tenants.
/// </summary>
public sealed class CustomTenantExceptions
{
    /// <summary>
    /// Exception para tenant não encontrado.
    /// </summary>
    public sealed class NotFoundTenantException : BaseException
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        public NotFoundTenantException(
            object dados = null)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, [
                   new("Tenant não encontrado na base de dados!")
               ]);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="notificacoes"></param>
        public NotFoundTenantException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, notificacoes);
        }
    }
}

/// <summary>
/// Exception para usuarios sem permissões a um tenant.
/// </summary>
public sealed class NotPermissionTenantException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public NotPermissionTenantException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Unauthorized, dados, [
               new("Tenant não pose ser manipulado por esse usuário!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public NotPermissionTenantException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.NotFound, dados, notificacoes);
    }
}

/// <summary>
/// Exception para tenant já cadastrado.
/// </summary>
public sealed class DuplicatedTenantException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public DuplicatedTenantException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Ambiguous, dados, [
               new("Tenant com o mesmo nome já cadastrado!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public DuplicatedTenantException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.Ambiguous, dados, notificacoes);
    }
}

/// <summary>
/// Exception para tenant com falha na criação.
/// </summary>
public sealed class CreateTenantFailedException : BaseException
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    public CreateTenantFailedException(
        object dados = null)
    {
        Response = new ErrorResponse
           (HttpStatusCode.BadRequest, dados, [
               new("Falha na criação do Tenant!")
           ]);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="dados"></param>
    /// <param name="notificacoes"></param>
    public CreateTenantFailedException(
        object dados, List<DataNotifications> notificacoes)
    {
        Response = new ErrorResponse
           (HttpStatusCode.BadRequest, dados, notificacoes);
    }
}
