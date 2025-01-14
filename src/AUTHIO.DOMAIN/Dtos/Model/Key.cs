using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Jwa;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AUTHIO.DOMAIN.Dtos.Model;

/// <summary>
/// Classe de key.
/// </summary>
[DebuggerDisplay("{Type}-{KeyId}")]
public class KeyMaterial : IEntityClientNullAble
{
    /// <summary>
    /// ctor.
    /// </summary>
    public KeyMaterial() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="cryptographicKey"></param>
    public KeyMaterial(
        CryptographicKey cryptographicKey, Guid? clientId)
    {
        CreationDate = DateTime.UtcNow;

        Parameters = JsonSerializer.Serialize(
            cryptographicKey.GetJsonWebKey(), typeof(JsonWebKey));

        Type = cryptographicKey.Algorithm.Kty();

        AlgorithmType = cryptographicKey.Algorithm.AlgorithmType;

        KeyId = cryptographicKey.Key.KeyId;

        ClientId = clientId;
    }

    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Id da chave.
    /// </summary>
    public string KeyId { get; set; }

    /// <summary>
    /// Id do Client.
    /// </summary>
    public Guid? ClientId {  get; set; }

    /// <summary>
    /// Tipo
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Tipo do algoritimo.
    /// </summary>
    public AlgorithmType AlgorithmType { get; set; }

    /// <summary>
    /// Parametros.
    /// </summary>
    public string Parameters { get; set; }

    /// <summary>
    /// Revogada.
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Razão para ser revogada.
    /// </summary>
    public string RevokedReason { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Expirar em.
    /// </summary>
    public DateTime? ExpiredAt { get; set; }

    /// <summary>
    /// Recuperar chave segura.
    /// </summary>
    /// <returns></returns>
    public JsonWebKey GetSecurityKey() 
        => JsonWebKey.Create(Parameters);

    /// <summary>
    /// Revogar,
    /// </summary>
    /// <param name="reason"></param>
    public void Revoke(string reason = default)
    {
        var jsonWebKey = GetSecurityKey();
        var publicWebKey = PublicJsonWebKey.FromJwk(jsonWebKey);

        ExpiredAt = DateTime.UtcNow;
        IsRevoked = true;
        RevokedReason = reason;

        JsonSerializerOptions jsonSerializerOptions = 
            new() { 
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault 
            };

        JsonSerializerOptions options = jsonSerializerOptions;

        Parameters = JsonSerializer.Serialize(
            publicWebKey.ToNativeJwk(), options);
    }

    /// <summary>
    /// Expirado.
    /// </summary>
    /// <param name="valueDaysUntilExpire"></param>
    /// <returns></returns>
    public bool IsExpired(int valueDaysUntilExpire) 
        => CreationDate.AddDays(
            valueDaysUntilExpire) < DateTime.UtcNow.Date;

    /// <summary>
    /// Cahve segura.
    /// </summary>
    /// <param name="value"></param>
    public static implicit operator SecurityKey(
        KeyMaterial value) => value.GetSecurityKey();
}