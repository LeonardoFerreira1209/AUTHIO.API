using AUTHIO.DOMAIN.Dtos.Email;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Cria um DefaultEmailMessage.
/// </summary>
public static class CreateDefaultEmailMessage
{
    /// <summary>
    /// Cria uma instância com htmlContent.
    /// </summary>
    /// <param name="fromName"></param>
    /// <param name="fromEmail"></param>
    /// <param name="toName"></param>
    /// <param name="toEmail"></param>
    /// <param name="subject"></param>
    /// <param name="plainTextContent"></param>
    /// <param name="htmlContent"></param>
    public static DefaultEmailMessage CreateWithHtmlContent(string fromName, string fromEmail,
        string toName, string toEmail, string subject, string plainTextContent, string htmlContent)
            => new DefaultEmailMessageBuilder()
                    .AddFrom(fromName, fromEmail)
                        .AddTo(toName, toEmail)
                            .AddSubject(subject)
                                .AddPlainTextContent(plainTextContent)
                                    .AddHtmlContent(htmlContent).Builder();
}
