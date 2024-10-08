using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Model;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Jwa;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Service de Jwt.
/// </summary>
/// <param name="store"></param>
/// <param name="cryptoService"></param>
/// <param name="contextService"></param>
/// <param name="options"></param>
/// <param name="tenantRepository"></param>
public class JwtService(
    IJsonWebKeyStore store,
    ICryptoService cryptoService,
    IContextService contextService,
    IOptions<JwtOptions> options,
    ITenantRepository tenantRepository) : IJwtService
{
    private readonly IJsonWebKeyStore _store = store;
    private readonly IOptions<JwtOptions> _options = options;

    private readonly TenantEntity _tenant
      = tenantRepository.GetAsync(
          x => x.TenantConfiguration.TenantKey == contextService.GetCurrentTenantKey())?.Result;

    /// <summary>
    /// Gerar chave.
    /// </summary>
    /// <returns></returns>
    public async Task<SecurityKey> GenerateKey()
    {
        var tenantTokenConfiguration =
            _tenant?.TenantConfiguration
                ?.TenantTokenConfiguration;

        var encrypted = tenantTokenConfiguration?.Encrypted ?? false;

        JwtType jwtType = encrypted
            ? JwtType.Jwe
            : JwtType.Jws;

        AlgorithmType algorithmType = encrypted
            ? tenantTokenConfiguration?.AlgorithmJweType ?? _options.Value.Jwe.AlgorithmType
            : tenantTokenConfiguration?.AlgorithmJwsType ?? _options.Value.Jws.AlgorithmType;

        var key = new CryptographicKey(
            cryptoService,
                Algorithm.Create(algorithmType, jwtType)
        );

        var model = new KeyMaterial(key, _tenant?.Id);

        await _store.Store(model);

        return model.GetSecurityKey();
    }

    /// <summary>
    /// Recuperar chave de segurança atual.
    /// </summary>
    /// <returns></returns>
    public async Task<SecurityKey> GetCurrentSecurityKey()
    {
        var current = await _store.GetCurrent();

        if (NeedsUpdate(current))
        {
            // According NIST - https://nvlpubs.nist.gov/nistpubs/SpecialPublications/NIST.SP.800-57pt1r4.pdf - Private key should be removed when no longer needs
            await _store.Revoke(current);
            var newKey = await GenerateKey();
            return newKey;
        }

        // options has change. Change current key
        if (!await CheckCompatibility(current))
            current = await _store.GetCurrent();

        return current;
    }

    /// <summary>
    /// Recupera credenciais assinada atuais.
    /// </summary>
    /// <returns></returns>
    public async Task<SigningCredentials> GetCurrentSigningCredentials()
    {
        var tenantTokenConfiguration =
           _tenant?.TenantConfiguration
               ?.TenantTokenConfiguration;

        var current = await GetCurrentSecurityKey();

        return new SigningCredentials(current,
            tenantTokenConfiguration is not null
                ? Algorithm.Create(tenantTokenConfiguration.AlgorithmJwsType, JwtType.Jws)
                : _options.Value.Jws
        );
    }

    /// <summary>
    /// Recupera credenciais assinada encripitadas atuais.
    /// </summary>
    /// <returns></returns>
    public async Task<EncryptingCredentials> GetCurrentEncryptingCredentials()
    {
        var tenantTokenConfiguration =
            _tenant?.TenantConfiguration
                ?.TenantTokenConfiguration;

        var current = await GetCurrentSecurityKey();

        var jwe = tenantTokenConfiguration is not null
                ? Algorithm.Create(tenantTokenConfiguration.AlgorithmJweType, JwtType.Jwe)
                : _options.Value.Jwe;

        return new EncryptingCredentials(current, jwe.Alg, jwe.EncryptionAlgorithmContent);
    }

    /// <summary>
    /// Recuperar ultimas chaves.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public Task<ReadOnlyCollection<KeyMaterial>> GetLastKeys(int? i = 2)
    {
        return _store.GetLastKeys(
            i ?? _options.Value.AlgorithmsToKeep
        );
    }

    /// <summary>
    /// Recupear compatibilidade.
    /// </summary>
    /// <param name="currentKey"></param>
    /// <returns></returns>
    private async Task<bool> CheckCompatibility(KeyMaterial currentKey)
    {
        var tenantTokenConfiguration =
           _tenant?.TenantConfiguration
               ?.TenantTokenConfiguration;

        var encrypted = tenantTokenConfiguration?.Encrypted ?? false;

        JwtType jwtType = encrypted
            ? JwtType.Jwe
            : JwtType.Jws;

        AlgorithmType algorithmType = encrypted
            ? tenantTokenConfiguration?.AlgorithmJweType ?? _options.Value.Jwe.AlgorithmType
            : tenantTokenConfiguration?.AlgorithmJwsType ?? _options.Value.Jws.AlgorithmType;

        Algorithm jwtAlgorithm =
            Algorithm.Create(algorithmType, jwtType);

        if (currentKey.Type != jwtAlgorithm.Kty()
           || currentKey.AlgorithmType != jwtAlgorithm.AlgorithmType)
        {
            await _store.Revoke(currentKey, "Changed encrypt alghorithm");

            await GenerateKey();

            return false;
        }

        return true;
    }

    /// <summary>
    /// Revogar chave.
    /// </summary>
    /// <param name="keyId"></param>
    /// <param name="reason"></param>
    /// <returns></returns>
    public async Task RevokeKey(string keyId, string reason = null)
    {
        var key = await _store.Get(keyId);

        await _store.Revoke(key, reason);
    }

    /// <summary>
    /// Gerar nova chave.
    /// </summary>
    /// <returns></returns>
    public async Task<SecurityKey> GenerateNewKey()
    {
        var oldCurrent = await _store.GetCurrent();

        await _store.Revoke(oldCurrent);

        return await GenerateKey();
    }

    /// <summary>
    /// Precisa de atualização ?.
    /// </summary>
    /// <param name="current"></param>
    /// <returns></returns>
    private bool NeedsUpdate(KeyMaterial current)
    {
        return current == null
            || current.IsExpired(_options.Value.DaysUntilExpire) 
            || current.IsRevoked;
    }
}
