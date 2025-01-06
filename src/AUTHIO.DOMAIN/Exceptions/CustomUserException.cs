using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Exceptions.Base;
using System.Net;

namespace AUTHIO.DOMAIN.Exceptions;

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
            object dados = null)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, [
                   new("Usuário não autenticado!")
               ]);
        }

        public UnauthorizedUserException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, notificacoes);
        }
    }

    public class ForbiddendUserException : BaseException
    {
        public ForbiddendUserException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Forbidden, dados, [
                   new("Usuário não tem as permissões mecessárias!")
               ]);
        }

        public ForbiddendUserException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Forbidden, dados, notificacoes);
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
               (HttpStatusCode.Unauthorized, dados, [
                   new("Token expirado!"),
               ]);
        }

        public UnauthorizedTokenLifetimeException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.Unauthorized, dados, notificacoes);
        }
    }


    /// <summary>
    /// Exception para usuário não auenticado não encontrado.
    /// </summary>
    public class AuthenticatedUserNotFoundException : BaseException
    {
        public AuthenticatedUserNotFoundException(
            object dados = null)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, [
                   new("Não há usuário autenticado!")
               ]);
        }

        public AuthenticatedUserNotFoundException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para usuário não encontrado.
    /// </summary>
    public class NotFoundUserException : BaseException
    {
        public NotFoundUserException(
            object dados = null)
        {
            Response = new ErrorResponse
               (HttpStatusCode.NotFound, dados, [
                   new("Dados do usuário não encontrado!")
               ]);
        }

        public NotFoundUserException(
            object dados, List<DataNotifications> notificacoes)
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
               (HttpStatusCode.NotFound, dados, [
                   new("Roles não econtradas!")
               ]);
        }

        public NotFoundRoleException(
            object dados, List<DataNotifications> notificacoes)
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
               (HttpStatusCode.BadRequest, dados, [
                   new("Dados do usuário incorretos!")
               ]);
        }

        public InvalidUserAuthenticationException(
            object dados, List<DataNotifications> notificacoes)
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
                (HttpStatusCode.Locked, dados, [
                   new("Usúario está bloqueado, aguarde alguns minutos e tente novamente!")
               ]);
        }

        public LockedOutAuthenticationException(
           object dados, List<DataNotifications> notificacoes)
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
               (HttpStatusCode.Unauthorized, dados, [
                   new("Usuário não está habilitado, confirme o e-mail!")
               ]);
        }

        public IsNotAllowedAuthenticationException(
            object dados, List<DataNotifications> notificacoes)
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
               (HttpStatusCode.Unauthorized, dados, [
                   new("Autenticação de dois fatores necessária!")
               ]);
        }

        public RequiresTwoFactorAuthenticationException(
            object dados, List<DataNotifications> notificacoes)
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
               (HttpStatusCode.BadRequest, dados, [
                   new("Códgo de confirmação inserido incorreto ou expirado!")
               ]);
        }

        public IncorrectConfirmationCodeAuthenticationException(
            object dados, List<DataNotifications> notificacoes)
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
               (HttpStatusCode.BadRequest, dados, [
                   new("Erro na geração do token JWT!")
               ]);
        }

        public TokenJwtException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para falaha na criação de usuários.
    /// </summary>
    public class CreateUserFailedException : BaseException
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        public CreateUserFailedException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, [
                   new("Falha na criação do usuário!")
               ]);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="notificacoes"></param>
        public CreateUserFailedException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, notificacoes);
        }
    }

    /// <summary>
    /// Exception para falaha na atualização de usuários.
    /// </summary>
    public class UpdateUserFailedException : BaseException
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        public UpdateUserFailedException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, [
                   new("Falha na criação do usuário!")
               ]);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="notificacoes"></param>
        public UpdateUserFailedException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, notificacoes);
        }
    }

    public class UserToRoleFailedException : BaseException
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        public UserToRoleFailedException(
            object dados)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, [
                   new("Falha ao adicionar role ao usuário!")
               ]);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dados"></param>
        /// <param name="notificacoes"></param>
        public UserToRoleFailedException(
            object dados, List<DataNotifications> notificacoes)
        {
            Response = new ErrorResponse
               (HttpStatusCode.BadRequest, dados, notificacoes);
        }
    }
}
