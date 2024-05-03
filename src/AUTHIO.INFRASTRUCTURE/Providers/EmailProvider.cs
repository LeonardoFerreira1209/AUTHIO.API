using AUTHIO.DOMAIN.Contracts.Providers;
using AUTHIO.DOMAIN.Dtos.Email;

namespace AUTHIO.INFRASTRUCTURE.Providers;

/// <summary>
/// Classe de provedor de e-mail defalt.
/// </summary>
public class EmailProvider : IEmailProvider
{
    /// <summary>
    /// Envia e-mail.
    /// </summary>
    /// <param name="message"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void SendEmail(DefaultEmailMessage message)
    {
        throw new NotImplementedException();
    }
}
