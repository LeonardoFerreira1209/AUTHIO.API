using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Helpers.Jwa;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace AUTHIO.INFRASTRUCTURE.Services;

public class CryptoService : ICryptoService
{
    /// <summary>
    /// Criar chave de segurança.
    /// </summary>
    /// <param name="keySize"></param>
    /// <returns></returns>
    public RsaSecurityKey CreateRsaSecurityKey(int keySize = 3072)
    {
        return new RsaSecurityKey(RSA.Create(keySize))
        {
            KeyId = CreateUniqueId()
        };
    }

    /// <summary>
    /// Criar um id unico.
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public string CreateUniqueId(int length = 16) 
        => Base64UrlEncoder.Encode(CreateRandomKey(length));

    /// <summary>
    /// Criar chave de segurança ECDsa.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public ECDsaSecurityKey CreateECDsaSecurityKey(Algorithm algorithm)
    {
        var curve = algorithm.Curve;

        if (string.IsNullOrEmpty(algorithm.Curve))
            curve = GetCurveType(algorithm);

        return new ECDsaSecurityKey(
            ECDsa.Create(GetNamedECCurve(curve))) {

            KeyId = CreateUniqueId()
        };
    }

    /// <summary>
    /// Recuperar um CurveType.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public string GetCurveType(Algorithm algorithm)
    {
        return algorithm.Alg switch
        {
            SecurityAlgorithms.EcdsaSha256 => JsonWebKeyECTypes.P256,
            SecurityAlgorithms.EcdsaSha384 => JsonWebKeyECTypes.P384,
            SecurityAlgorithms.EcdsaSha512 => JsonWebKeyECTypes.P521,
            _ => throw new InvalidOperationException($"Unsupported curve type for {algorithm}")
        };
    }

    /// <summary>
    /// Criar chave de segurança Hmac.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public HMAC CreateHmacSecurityKey(Algorithm algorithm)
    {
        var hmac = algorithm.Alg switch
        {
            SecurityAlgorithms.HmacSha256 => (HMAC)new HMACSHA256(CreateRandomKey(64)),
            SecurityAlgorithms.HmacSha384 => new HMACSHA384(CreateRandomKey(128)),
            SecurityAlgorithms.HmacSha512 => new HMACSHA512(CreateRandomKey(128)),
            _ => throw new CryptographicException("Could not create HMAC key based on algorithm " + algorithm +
                                                  " (Could not parse expected SHA version)")
        };

        return hmac;
    }


    /// <summary>
    /// Criar chave de segurança AESS.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public Aes CreateAESSecurityKey(Algorithm algorithm)
    {
        var aesKey = Aes.Create();
        var aesKeySize = algorithm.Alg switch
        {
            SecurityAlgorithms.Aes128KW => 128,
            SecurityAlgorithms.Aes256KW => 256,
            _ => throw new CryptographicException("Could not create AES key based on algorithm " + algorithm)
        };

        aesKey.KeySize = aesKeySize;

        aesKey.GenerateKey();

        return aesKey;
    }

    /// <summary>
    /// Recuperar um ECCURVE nomeado.
    /// </summary>
    /// <param name="curveId"></param>
    /// <returns></returns>
    public ECCurve GetNamedECCurve(string curveId)
    {
        if (string.IsNullOrEmpty(curveId))
            throw LogHelper.LogArgumentNullException(nameof(curveId));

        // treat 512 as 521. It was a bug in .NET Release.
        return curveId switch
        {
            JsonWebKeyECTypes.P256 => ECCurve.NamedCurves.nistP256,
            JsonWebKeyECTypes.P384 => ECCurve.NamedCurves.nistP384,
            JsonWebKeyECTypes.P512 => ECCurve.NamedCurves.nistP521,
            JsonWebKeyECTypes.P521 => ECCurve.NamedCurves.nistP521,
            _ => throw LogHelper.LogExceptionMessage(new ArgumentException(curveId))
        };
    }

    /// <summary>
    /// Criar uma chave random key.
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public byte[] CreateRandomKey(int length)
    {
        byte[] data = new byte[length];

        RandomNumberGenerator.Fill(data);

        return data;
    }
}