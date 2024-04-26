namespace AUTHIO.DOMAIN.Dtos.Email;

/// <summary>
/// Classe mensagem de e-mail.
/// </summary>
public class DefaultEmailMessage(DefaultEmailAddres from,
    DefaultEmailAddres to, string subject, string body,
    string templateId, string plainTextContent, string htmlContent)
{
    /// <summary>
    /// Remetente.
    /// </summary>
    public DefaultEmailAddres From { get; set; } = from;

    /// <summary>
    /// Destinatario.
    /// </summary>
    public DefaultEmailAddres To { get; set; } = to;

    /// <summary>
    /// Assunto.
    /// </summary>
    public string Subject { get; set; } = subject;

    /// <summary>
    /// Corpo.
    /// </summary>
    public string Body { get; set; } = body;

    /// <summary>
    /// Id do template.
    /// </summary>
    public string TemplateId { get; set; } = templateId;

    /// <summary>
    /// Texto dp tipo Mime Type of text/plain.
    /// </summary>
    public string PlainTextContent { get; set; } = plainTextContent;

    /// <summary>
    /// Conteudo Html do tipo Mime Type of text/html.
    /// </summary>
    public string HtmlContent { get; set; } = htmlContent;
}
