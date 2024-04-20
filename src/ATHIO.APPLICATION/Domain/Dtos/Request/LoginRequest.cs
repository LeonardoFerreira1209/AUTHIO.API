namespace AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST;

/// <summary>
/// Request para Fazer o login do usuário.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="email"></param>
/// <param name="password"></param>
public class LoginRequest(
    string email, string password)
{

    /// <summary>
    /// Username do usuário
    /// </summary>
    public string Username { get; set; } = email;

    /// <summary>
    /// Senha do usuário
    /// </summary>
    /// 
    public string Password { get; set; } = password;
}
