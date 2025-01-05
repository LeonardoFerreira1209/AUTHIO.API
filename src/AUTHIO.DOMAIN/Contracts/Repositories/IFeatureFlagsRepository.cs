using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Interface de feature flags
/// </summary>
public interface IFeatureFlagsRepository
    : IGenerictEntityCoreRepository<FeatureFlagsEntity>
{
    /// <summary>
    /// Retorna a defeinição da feature.
    /// </summary>
    /// <param name="featureName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FeatureFlagsEntity> GetFeatureDefinitionAsync(
        string featureName,
        CancellationToken cancellationToken
    );

    /// <summary>
    /// Retonrna todas as featuire flags.
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<FeatureFlagsEntity> GetAllFeatureDefinitionsAsync();
}
