namespace AUTHIO.DOMAIN.Dtos.Email;

/// <summary>
/// Classe que representa Endereço de e-mail e nome.
/// </summary>
public class EmailAddres : IEmailAddress
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
