using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Email;
using AUTHIO.DOMAIN.Helpers.Extensions;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AUTHIO.INFRASTRUCTURE.Providers;

/// <summary>
/// Classe de provider de email do SendGrid.
/// </summary>
public class SendGridEmailProvider(
    IOptions<AppSettings> appSettings) : IEmailProvider
{
    /// <summary>
    /// Método de envio de e-mail.
    /// </summary>
    /// <param name="message"></param>
    public async Task SendEmail(DefaultEmailMessage message)
    {
        var client = new SendGridClient(
            appSettings.Value.Email.SendGrid.ApiKey);

        var a = await client.SendEmailAsync(MailHelper.CreateSingleEmail(
                message.From.ToSendGridEmailAddres(),
                message.To.ToSendGridEmailAddres(),
                message.Subject,
                message.PlainTextContent,
                message.HtmlContent
            ));

        if(a.IsSuccessStatusCode)
        {
            return;
        }
    }
}
