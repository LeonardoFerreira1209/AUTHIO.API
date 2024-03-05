namespace AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST;

/// <summary>
/// Request para Fazer o login do usuário.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="username"></param>
/// <param name="password"></param>
public class LoginRequest(
    string username, string password)
{

    /// <summary>
    /// Email do usuário
    /// </summary>
    public string Username { get; set; } = username;

    /// <summary>
    /// Senha do usuário
    /// </summary>
    /// 
    public string Password { get; set; } = password;
}
