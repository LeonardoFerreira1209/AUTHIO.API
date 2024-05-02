using AUTHIO.DOMAIN.Contracts.Providers;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Email;
using AUTHIO.DOMAIN.Helpers.Extensions;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;

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
        Log.Information($"ApiKey: {Environment.GetEnvironmentVariable("SENDGRID_APIKEY")}");

        var client = new SendGridClient(
            Environment.GetEnvironmentVariable("SENDGRID_APIKEY") 
                ?? appSettings.Value.Email.SendGrid.ApiKey);

        await client.SendEmailAsync(MailHelper.CreateSingleEmail(
                message.From.ToSendGridEmailAddres(),
                message.To.ToSendGridEmailAddres(),
                message.Subject,
                message.PlainTextContent,
                message.HtmlContent));
    }
}
