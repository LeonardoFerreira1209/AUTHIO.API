using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories;

/// <summary>
/// Interface de feature flags
/// </summary>
public interface IFeatureFlags
    : IGenerictEntityCoreRepository<FeatureFlagsEntity>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="featureName"></param>
    /// <returns></returns>
    Task<FeatureFlagsEntity> GetFeatureDefinitionAsync(string featureName);

    /// <summary>
    /// Retonrna todas as featuire flags.
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<FeatureFlagsEntity> GetAllFeatureDefinitionsAsync();
}
