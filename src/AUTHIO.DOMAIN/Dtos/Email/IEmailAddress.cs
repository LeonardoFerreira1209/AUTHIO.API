namespace AUTHIO.DOMAIN.Dtos.Email;

/// <summary>
/// Interface de mensagem de e-mail.
/// </summary>
public interface IEmailAddress
{
    /// <summary>
    /// Nome.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; }
}
