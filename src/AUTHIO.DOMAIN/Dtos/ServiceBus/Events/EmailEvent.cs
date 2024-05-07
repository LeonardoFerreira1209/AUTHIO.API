using AUTHIO.DOMAIN.Dtos.Email;

namespace AUTHIO.DOMAIN.Dtos.ServiceBus.Events;

/// <summary>
/// Classe de dados de envento de email.
/// </summary>
public class EmailEvent(DefaultEmailMessage defaultEmailMessage) 
        : DefaultEmailMessage(defaultEmailMessage.From, defaultEmailMessage.To, 
            defaultEmailMessage.Subject, defaultEmailMessage.Body, defaultEmailMessage.TemplateId, 
            defaultEmailMessage.PlainTextContent, defaultEmailMessage.HtmlContent)
{

}
