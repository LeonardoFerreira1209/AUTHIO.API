using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Dtos.Configurations;

/// <summary>
/// Classe de 
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Algoritimo Jws.
    /// </summary>
    public Algorithm Jws { get; set; } 
        = Algorithm.Create(AlgorithmType.RSA, JwtType.Jws);

    /// <summary>
    /// Algoritimo Jwe.
    /// </summary>
    public Algorithm Jwe { get; set; } 
        = Algorithm.Create(AlgorithmType.RSA, JwtType.Jwe);

    /// <summary>
    /// Dias para expiração.
    /// </summary>
    public int DaysUntilExpire { get; set; } = 90;

    /// <summary>
    /// Perfixo da chave.
    /// </summary>
    public string KeyPrefix { get; set; } = $"{Environment.MachineName}_";

    /// <summary>
    /// Algoritimos para manter algoritimo. 
    /// </summary>
    public int AlgorithmsToKeep { get; set; } = 2;

    /// <summary>
    /// Tempo de cache.
    /// </summary>
    public TimeSpan CacheTime { get; set; } = TimeSpan.FromMinutes(15);
}