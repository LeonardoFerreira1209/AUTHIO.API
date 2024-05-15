using AUTHIO.DOMAIN.Dtos.Email;

namespace AUTHIO.DOMAIN.Contracts.Providers.Email;

/// <summary>
/// Interface de provedor de email.
/// </summary>
public interface IEmailProvider
{
    /// <summary>
    /// Método de envio de email.
    /// </summary>
    /// <param name="message"></param>
    Task SendEmailAsync(DefaultEmailMessage message);
}
