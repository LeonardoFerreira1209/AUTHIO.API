﻿using Microsoft.IdentityModel.Tokens;

namespace AUTHIO.DOMAIN.Helpers.Jwa;

/// <summary>
/// Classe de Algoritimo de criptografia.
/// </summary>
public class Algorithm
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="algorithm"></param>
    /// <exception cref="NotSupportedException"></exception>
    private Algorithm(string algorithm)
    {
        switch (algorithm)
        {
            case EncryptionAlgorithmKey.Aes128KW:
            case EncryptionAlgorithmKey.Aes256KW:
                AlgorithmType = AlgorithmType.AES;
                CryptographyType = CryptographyType.Encryption;
                break;

            case EncryptionAlgorithmKey.RsaPKCS1:
            case EncryptionAlgorithmKey.RsaOAEP:
                AlgorithmType = AlgorithmType.RSA;
                CryptographyType = CryptographyType.Encryption;
                break;
            case DigitalSubscriptionsAlgorithm.EcdsaSha256:
            case DigitalSubscriptionsAlgorithm.EcdsaSha384:
            case DigitalSubscriptionsAlgorithm.EcdsaSha512:
                AlgorithmType = AlgorithmType.ECDsa;
                CryptographyType = CryptographyType.DigitalSubscription;
                break;

            case DigitalSubscriptionsAlgorithm.HmacSha256:
            case DigitalSubscriptionsAlgorithm.HmacSha384:
            case DigitalSubscriptionsAlgorithm.HmacSha512:
                AlgorithmType = AlgorithmType.HMAC;
                CryptographyType = CryptographyType.DigitalSubscription;
                break;

            case DigitalSubscriptionsAlgorithm.RsaSha256:
            case DigitalSubscriptionsAlgorithm.RsaSha384:
            case DigitalSubscriptionsAlgorithm.RsaSha512:
            case DigitalSubscriptionsAlgorithm.RsaSsaPssSha256:
            case DigitalSubscriptionsAlgorithm.RsaSsaPssSha384:
            case DigitalSubscriptionsAlgorithm.RsaSsaPssSha512:
                AlgorithmType = AlgorithmType.RSA;
                CryptographyType = CryptographyType.DigitalSubscription;
                break;

            default:
                throw new NotSupportedException($"Not supported algorithm {algorithm}");
        }

        Alg = algorithm;
    }

    /// <summary>
    /// ctor
    /// </summary>
    private Algorithm()
    {
        AlgorithmType = AlgorithmType.RSA;
    }

    /// <summary>
    /// Conteudo do algoritimo criptografado.
    /// </summary>
    public EncryptionAlgorithmContent EncryptionAlgorithmContent { get; set; }

    /// <summary>
    /// Tipo do algoritimo.
    /// </summary>
    public AlgorithmType AlgorithmType { get; internal set; }

    /// <summary>
    /// Tipo da criptografia.
    /// </summary>
    public CryptographyType CryptographyType { get; internal set; }

    /// <summary>
    /// Tipo do Jwt.
    /// </summary>
    public JwtType JwtType => CryptographyType
        == CryptographyType.Encryption ? JwtType.Jwe : JwtType.Jws;

    /// <summary>
    /// Algoritimo.
    /// </summary>
    public string Alg { get; internal set; }

    /// <summary>
    /// Curve.
    /// </summary>
    public string Curve { get; set; }

    /// <summary>
    /// Com curve.
    /// </summary>
    /// <param name="curve"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Algorithm WithCurve(string curve)
    {
        if (AlgorithmType != AlgorithmType.ECDsa)
            throw new InvalidOperationException("Only Elliptic Curves accept curves");

        Curve = curve;
        return this;
    }

    /// <summary>
    /// Com conteudo criptografado.
    /// </summary>
    /// <param name="enc"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public Algorithm WithContentEncryption(EncryptionAlgorithmContent enc)
    {
        if (CryptographyType == CryptographyType.DigitalSubscription)
            throw new InvalidOperationException("Only Json Web Encryption has enc param");

        EncryptionAlgorithmContent = (string)enc switch
        {
            EncryptionAlgorithmContent.Aes128CbcHmacSha256 
                or EncryptionAlgorithmContent.Aes128Gcm 
                or EncryptionAlgorithmContent.Aes192CbcHmacSha384 
                or EncryptionAlgorithmContent.Aes192Gcm 
                or EncryptionAlgorithmContent.Aes256CbcHmacSha512 
                or EncryptionAlgorithmContent.Aes256Gcm => enc,
            _ => throw new NotSupportedException($"Not supported encryption algorithm {enc}"),
        };

        return this;
    }


    /// <summary>
    /// See RFC 7518 - JSON Web Algorithms (JWA) - Section 6.1. "kty" (Key Type) Parameter Values
    /// </summary>
    public string Kty()
    {
        return AlgorithmType switch
        {
            AlgorithmType.RSA => JsonWebAlgorithmsKeyTypes.RSA,
            AlgorithmType.ECDsa => JsonWebAlgorithmsKeyTypes.EllipticCurve,
            AlgorithmType.HMAC => JsonWebAlgorithmsKeyTypes.Octet,
            AlgorithmType.AES => JsonWebAlgorithmsKeyTypes.Octet,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Criar algoritimo.
    /// </summary>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    public static Algorithm Create(string algorithm) => new(algorithm);

    /// <summary>
    /// Criar algoritimo baseado em um tipo e tipo jwt.
    /// </summary>
    /// <param name="algorithmType"></param>
    /// <param name="jwtType"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Algorithm Create(
        AlgorithmType algorithmType, JwtType jwtType)
    {
        if (jwtType == JwtType.Both)
            return new Algorithm();

        if (jwtType == JwtType.Jws)
            return algorithmType switch
            {
                AlgorithmType.RSA => new Algorithm(DigitalSubscriptionsAlgorithm.RsaSsaPssSha256),
                AlgorithmType.ECDsa => new Algorithm(DigitalSubscriptionsAlgorithm.EcdsaSha256).WithCurve(JsonWebKeyECTypes.P256),
                AlgorithmType.HMAC => new Algorithm(DigitalSubscriptionsAlgorithm.HmacSha256),
                _ => throw new InvalidOperationException($"Invalid algorithm for Json Web Subscription (JWS): {algorithmType}")
            };

        return algorithmType switch
        {
            AlgorithmType.RSA => new Algorithm(EncryptionAlgorithmKey.RsaOAEP).WithContentEncryption(EncryptionAlgorithmContent.Aes128CbcHmacSha256),
            AlgorithmType.AES => new Algorithm(EncryptionAlgorithmKey.Aes128KW).WithContentEncryption(EncryptionAlgorithmContent.Aes128CbcHmacSha256),
            _ => throw new InvalidOperationException($"Invalid algorithm for Json Web Encryption (JWE): {algorithmType}")
        };
    }

    /// <summary>
    /// Valor do algoritimo.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator string(Algorithm value) => value.Alg;

    /// <summary>
    /// Valor do algoritimo.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator Algorithm(string value) => new(value);
}