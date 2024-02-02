using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.Domain.Exceptions.Base;
using System.Net;

namespace AUTHIO.APPLICATION.Domain.Exceptions;

/// <summary>
/// Exceptions
/// </summary>
public class CustomUserException
{
    /// <summary>
    /// Exception para usuário não encontrado.
    /// </summary>
    public class UnauthorizedUserException : BaseException
    {
        public UnauthorizedUserException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, new List<DadosNotificacao>() {
                   new("Usuário não autorizado!"),
                   new("Sem as permissões necessárias!")
               });
        }

        public UnauthorizedUserException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, notificacoes);
        }

    }

    /// <summary>
    /// Exception para Token expirado não encontrado.
    /// </summary>
    public class UnauthorizedTokenLifetimeException : BaseException
    {
        public UnauthorizedTokenLifetimeException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, new List<DadosNotificacao>() {
                   new("Token expirado!"),
               });
        }

        public UnauthorizedTokenLifetimeException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, notificacoes);
        }

    }

    /// <summary>
    /// Exception para usuário não encontrado.
    /// </summary>
    public class NotFoundUserException : BaseException
    {
        public NotFoundUserException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, new List<DadosNotificacao>() {
                   new("Dados do usuário não encontrado!")
               });
        }

        public NotFoundUserException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para role não encontrada.
    /// </summary>
    public class NotFoundRoleException : BaseException
    {
        public NotFoundRoleException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, new List<DadosNotificacao>() {
                   new("Roles não econtradas!")
               });
        }

        public NotFoundRoleException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para Criação de usuário inválido.
    /// </summary>
    public class InvalidUserAuthenticationException : BaseException
    {
        public InvalidUserAuthenticationException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, new List<DadosNotificacao>() {
                   new("Dados do usuário incorretos!")
               });
        }

        public InvalidUserAuthenticationException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para usuário bloqueado.
    /// </summary>
    public class LockedOutAuthenticationException : BaseException
    {
        public LockedOutAuthenticationException(
            object dados)
        {
            Response = new ErrorResponse
                (HttpStatusCode.Locked, dados, new List<DadosNotificacao>() {
                   new("Usúario está bloqueado, aguarde alguns minutos e tente novamente!")
               });
        }

        public LockedOutAuthenticationException(
           object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
                (HttpStatusCode.Locked, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para usuário não habilitado.
    /// </summary>
    public class IsNotAllowedAuthenticationException : BaseException
    {
        public IsNotAllowedAuthenticationException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, new List<DadosNotificacao>() {
                   new("Usuário não está habilitado, confirme o e-mail!")
               });
        }

        public IsNotAllowedAuthenticationException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para usuário que necessita da autenticação de dois fatores.
    /// </summary>
    public class RequiresTwoFactorAuthenticationException : BaseException
    {
        public RequiresTwoFactorAuthenticationException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, new List<DadosNotificacao>() {
                   new("Autenticação de dois fatores necessária!")
               });
        }

        public RequiresTwoFactorAuthenticationException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, notificacoes);
        }
    }

    public class IncorrectConfirmationCodeAuthenticationException : BaseException
    {
        public IncorrectConfirmationCodeAuthenticationException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, new List<DadosNotificacao>() {
                   new("Códgo de confirmação inserido incorreto ou expirado!")
               });
        }

        public IncorrectConfirmationCodeAuthenticationException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para falha na geração de tokenJwt.
    /// </summary>
    public class TokenJwtException : BaseException
    {
        public TokenJwtException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, new List<DadosNotificacao>() {
                   new("Erro na geração do token JWT!")
               });
        }

        public TokenJwtException(
            object dados, List<DadosNotificacao> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, notificacoes);
        }
    }
}
