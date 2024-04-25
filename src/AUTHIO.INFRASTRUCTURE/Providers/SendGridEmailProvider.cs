using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Dtos.Email;
using SendGrid.Helpers.Mail;

namespace AUTHIO.INFRASTRUCTURE.Providers;

/// <summary>
/// Classe de provider de email do SendGrid.
/// </summary>
public class SendGridEmailProvider : IEmailProvider
{
    /// <summary>
    /// Método de envio de e-mail.
    /// </summary>
    /// <param name="message"></param>
    public void SendEmail(IEmailMessage message)
    {
        var a = new SendGridMessage()
        {

            From = new EmailAddress(message.From.Email, message.From.Name),
            TemplateId = message.TemplateId,
            Subject = message.Subject,

        };
        a.AddTo(message.To.Name);
    }
}
