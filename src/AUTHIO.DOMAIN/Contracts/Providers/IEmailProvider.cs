using AUTHIO.DOMAIN.Dtos.Email;

namespace AUTHIO.DOMAIN.Contracts.Providers;

/// <summary>
/// Interface de provedor de email.
/// </summary>
public interface IEmailProvider
{
    /// <summary>
    /// Método de envio de email.
    /// </summary>
    /// <param name="message"></param>
    Task SendEmail(DefaultEmailMessage message);
}
