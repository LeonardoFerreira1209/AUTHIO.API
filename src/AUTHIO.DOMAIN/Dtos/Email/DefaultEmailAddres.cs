namespace AUTHIO.DOMAIN.Dtos.Email;

/// <summary>
/// Classe que representa Endereço de e-mail e nome.
/// </summary>
public class DefaultEmailAddres(
    string name, string email)
{
    /// <summary>
    /// Nome.
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; } = email;
}
