namespace AUTHIO.DOMAIN.Constants;

/// <summary>
/// Constants do Jwks
/// </summary>
public class JwkContants
{
    /// <summary>
    /// Cache atual do Jwk.
    /// </summary>
    public static string CurrentJwkCache => $"AUTHIO-SECURITY-KEY";

    /// <summary>
    /// Cache do Jwks.
    /// </summary>
    public static string JwksCache => $"AUTHIO-JWKS";
}