using AUTHIO.DOMAIN.Dtos.Email;

namespace AUTHIO.DOMAIN.Contracts.Factories;

/// <summary>
/// Interface de provedor de email.
/// </summary>
public interface IEmailProvider
{
    /// <summary>
    /// Método de envio de email.
    /// </summary>
    /// <param name="message"></param>
    void SendEmail(IEmailMessage message);
}
