using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.Domain.Exceptions.Base;
using System.Net;

namespace AUTHIO.APPLICATION.Domain.Exceptions;

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
            object dados)
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
            object dados, List<DadosNotificacao> notificacoes)
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
            object dados)
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
            object dados, List<DadosNotificacao> notificacoes)
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
            object dados)
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
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, notificacoes);
        }
    }
}
