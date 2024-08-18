using AUTHIO.DOMAIN.Contracts.Jwt;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Dtos.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace AUTHIO.INFRASTRUCTURE.Services
{
    public class JwtService(
        IJsonWebKeyStore store, 
        ICryptoService cryptoService, 
        IContextService contextService,
        IOptions<JwtOptions> options,
        ITenantRepository tenantRepository) : IJwtService
    {
        private readonly IJsonWebKeyStore _store = store;
        private readonly IOptions<JwtOptions> _options = options;

        private readonly Guid? _currentTenantId
          = tenantRepository.GetAsync(
              x => x.TenantConfiguration.TenantKey == contextService.GetCurrentTenantKey())?.Result?.Id;

        /// <summary>
        /// Gerar chave.
        /// </summary>
        /// <returns></returns>
        public async Task<SecurityKey> GenerateKey()
        {
            var key = new CryptographicKey(cryptoService, _options.Value.Jws);

            var model = new KeyMaterial(key, _currentTenantId);
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
            var current = await GetCurrentSecurityKey();

            return new SigningCredentials(current, _options.Value.Jws);
        }

        /// <summary>
        /// Recupera credenciais assinada encripitadas atuais.
        /// </summary>
        /// <returns></returns>
        public async Task<EncryptingCredentials> GetCurrentEncryptingCredentials()
        {
            var current = await GetCurrentSecurityKey();

            return new EncryptingCredentials(current, _options.Value.Jwe.Alg, _options.Value.Jwe.EncryptionAlgorithmContent);
        }

        /// <summary>
        /// Recuperar ultimas chaves.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Task<ReadOnlyCollection<KeyMaterial>> GetLastKeys(int? i = null)
        {
            return _store.GetLastKeys(_options.Value.AlgorithmsToKeep);
        }

        /// <summary>
        /// Recupear compatibilidade.
        /// </summary>
        /// <param name="currentKey"></param>
        /// <returns></returns>
        private async Task<bool> CheckCompatibility(KeyMaterial currentKey)
        {
            if (currentKey.Type != _options.Value.Jws.Kty())
            {
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
}
