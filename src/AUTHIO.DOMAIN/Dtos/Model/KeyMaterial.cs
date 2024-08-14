using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Helpers.Jwa;
using Microsoft.IdentityModel.Tokens;

namespace AUTHIO.DOMAIN.Dtos.Model;

/// <summary>
/// Classe de criptografia de chave.
/// </summary>
public class CryptographicKey
{
    /// <summary>
    /// Instancia de serviço de cryptografia.
    /// </summary>
    private readonly ICryptoService _cryptoService;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="cryptoService"></param>
    /// <param name="algorithm"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public CryptographicKey(
        ICryptoService cryptoService, Algorithm algorithm)
    {
        _cryptoService = cryptoService;

        Algorithm = algorithm;
        Key = algorithm.AlgorithmType switch
        {
            AlgorithmType.RSA => GenerateRsa(),
            AlgorithmType.ECDsa => GenerateECDsa(algorithm),
            AlgorithmType.HMAC => GenerateHMAC(algorithm),
            AlgorithmType.AES => GenerateAES(algorithm),
            _ => throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null)
        };
    }

    /// <summary>
    /// Algoritimo.
    /// </summary>
    public Algorithm Algorithm { get; set; }

    /// <summary>
    /// Chave.
    /// </summary>
    public SecurityKey Key { get; set; }

    /// <summary>
    /// Recupera chave web do Json.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public JsonWebKey GetJsonWebKey() => Algorithm.AlgorithmType switch
    {
        AlgorithmType.RSA => JsonWebKeyConverter.ConvertFromRSASecurityKey((RsaSecurityKey)Key),
        AlgorithmType.ECDsa => JsonWebKeyConverter.ConvertFromECDsaSecurityKey((ECDsaSecurityKey)Key),
        AlgorithmType.HMAC => JsonWebKeyConverter.ConvertFromSymmetricSecurityKey((SymmetricSecurityKey)Key),
        AlgorithmType.AES => JsonWebKeyConverter.ConvertFromSymmetricSecurityKey((SymmetricSecurityKey)Key),
        _ => throw new ArgumentOutOfRangeException()
    };

    /// <summary>
    /// Gera um Rsa Security Key.
    /// </summary>
    /// <returns></returns>
    private RsaSecurityKey GenerateRsa() 
        => _cryptoService.CreateRsaSecurityKey();

    /// <summary>
    /// Gera um ECDsa Security Key.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    private ECDsaSecurityKey GenerateECDsa(Algorithm algorithm) 
        => _cryptoService.CreateECDsaSecurityKey(algorithm);

    /// <summary>
    /// Gera uma chave simetrica HMAC.
    /// </summary>
    /// <param name="jwsAlgorithms"></param>
    /// <returns></returns>
    private SymmetricSecurityKey GenerateHMAC(Algorithm jwsAlgorithms)
    {
        var key = 
            _cryptoService.CreateHmacSecurityKey(jwsAlgorithms);

        return new SymmetricSecurityKey(key.Key) {
            KeyId = _cryptoService.CreateUniqueId()
        };
    }

    /// <summary>
    /// Gera uma chave simetrica AES.
    /// </summary>
    /// <param name="jwsAlgorithms"></param>
    /// <returns></returns>
    private SymmetricSecurityKey GenerateAES(Algorithm jwsAlgorithms)
    {
        var key = 
            _cryptoService.CreateAESSecurityKey(jwsAlgorithms);

        return new SymmetricSecurityKey(key.Key) {
            KeyId = _cryptoService.CreateUniqueId()
        };
    }

    /// <summary>
    /// Chave secreta.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator SecurityKey(CryptographicKey value) => value.Key;
}