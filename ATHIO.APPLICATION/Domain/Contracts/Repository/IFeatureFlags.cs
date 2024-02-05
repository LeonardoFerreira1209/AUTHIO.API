using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Entity;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repository;

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
