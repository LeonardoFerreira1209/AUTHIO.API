using AUTHIO.DOMAIN.Dtos.Email;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de DefaultEmailMessage.
/// </summary>
public class DefaultEmailMessageBuilder
{
    private DefaultEmailAddres from;
    private DefaultEmailAddres to;
    private string subject;
    private string body;
    private string templateId;
    private string plainTextContent;
    private string htmlContent;

    /// <summary>
    /// Adiciona um from.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public DefaultEmailMessageBuilder AddFrom(string name, string email)
    {
        from = new(name, email);

        return this;
    }

    /// <summary>
    /// Adiciona um to.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public DefaultEmailMessageBuilder AddTo(string name, string email)
    {
        to = new(name, email);

        return this;
    }

    /// <summary>
    /// Adiciona um subject
    /// </summary>
    /// <param name="subject"></param>
    public DefaultEmailMessageBuilder AddSubject(string subject)
    {
        this.subject = subject;

        return this;
    }

    /// <summary>
    /// Adiciona o body.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    public DefaultEmailMessageBuilder AddBody(string body)
    {
        this.body = body;

        return this;
    }

    /// <summary>
    /// Adiciona o tempateId.
    /// </summary>
    /// <param name="templateId"></param>
    /// <returns></returns>
    public DefaultEmailMessageBuilder AddTemplateId(string templateId)
    {
        this.templateId = templateId;

        return this;
    }

    /// <summary>
    /// Adiciona o plainTextContent.
    /// </summary>
    /// <param name="plainTextContent"></param>
    /// <returns></returns>
    public DefaultEmailMessageBuilder AddPlainTextContent(string plainTextContent)
    {
        this.plainTextContent = plainTextContent;

        return this;
    }

    /// <summary>
    /// Adiciona o htmlContent.
    /// </summary>
    /// <param name="htmlContent"></param>
    /// <returns></returns>
    public DefaultEmailMessageBuilder AddHtmlContent(string htmlContent)
    {
        this.htmlContent = htmlContent;

        return this;
    }

    /// <summary>
    /// Cria um DefaultEmailMessage.
    /// </summary>
    /// <returns></returns>
    public DefaultEmailMessage Builder()
        => new(from, to, subject, body, 
            templateId, plainTextContent, htmlContent);
}
