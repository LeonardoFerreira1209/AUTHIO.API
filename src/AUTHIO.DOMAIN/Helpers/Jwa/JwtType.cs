namespace AUTHIO.DOMAIN.Helpers.Jwa;

/// <summary>
/// Jws usará algoritmos de assinatura digital
/// Usaremos algoritmos de criptografia
/// </summary>
public enum JwtType
{
    Jws = 1,
    Jwe = 2,
    Both = 3
}