using AUTHIO.DOMAIN.Helpers.Jwa;

namespace AUTHIO.DOMAIN.Dtos.Request;

/// <summary>
/// Request de atualização de token configuration.
/// </summary>
public sealed class UpdateClientTokenConfigurationRequest
{
    /// <summary>
    /// Id do Client.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// String de segurança para validação do token.
    /// </summary>
    public string SecurityKey { get; set; }

    /// <summary>
    /// String de issuer valido para validação do token.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// String de audience valido para validação do token.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Token deve ser encriptado.
    /// </summary>
    public bool Encrypted { get; set; }

    /// <summary>
    /// Tipo do algoritimo do token jws.
    /// </summary>
    public AlgorithmType AlgorithmJwsType { get; set; }

    /// <summary>
    /// Tipo do algoritimo do token jwe.
    /// </summary>
    public AlgorithmType AlgorithmJweType { get; set; }
}
