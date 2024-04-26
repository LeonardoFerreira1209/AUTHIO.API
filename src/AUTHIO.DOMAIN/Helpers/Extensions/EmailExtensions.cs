
using AUTHIO.DOMAIN.Dtos.Email;
using SendGrid.Helpers.Mail;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Extensão de classes de email.
/// </summary>
public static class EmailExtensions
{
    /// <summary>
    /// Convert um DefaultEmailMessage em SendGridMessage.
    /// </summary>
    /// <param name="emailMessage"></param>
    /// <returns></returns>
    public static SendGridMessage ToSendGridMessage(
        this DefaultEmailMessage emailMessage)
    {
        SendGridMessage sendGridMessage = new()
        {
            From = emailMessage.From.ToSendGridEmailAddres(),
            TemplateId = "d-a5a2d227be3a491ea863112e28b2ae84",/*emailMessage.TemplateId*/
            Subject = emailMessage.Subject,
            PlainTextContent = emailMessage.PlainTextContent,
            HtmlContent = emailMessage.HtmlContent
        };

        sendGridMessage.AddTo(
            emailMessage.To.ToSendGridEmailAddres());

        return sendGridMessage;
    }

    /// <summary>
    /// Convert um DefaultEmailAddres em EmailAddress do sendGrid.
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <returns></returns>
    public static EmailAddress ToSendGridEmailAddres(
        this DefaultEmailAddres emailAddress) => new()
        {
            Email = emailAddress.Email,
            Name = emailAddress.Name
        };
}
