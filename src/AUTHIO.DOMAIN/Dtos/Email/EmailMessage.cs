﻿namespace AUTHIO.DOMAIN.Dtos.Email;

/// <summary>
/// Classe mensagem de e-mail.
/// </summary>
public class EmailMessage : IEmailMessage
{
    /// <summary>
    /// Remetente.
    /// </summary>
    public IEmailAddress From { get; set; }

    /// <summary>
    /// Destinatario.
    /// </summary>
    public IEmailAddress To { get; set; }

    /// <summary>
    /// Assunto.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Corpo.
    /// </summary>
    public string Body { get; set; }

    /// <summary>
    /// Id do template.
    /// </summary>
    public string TemplateId { get; set; }
}
