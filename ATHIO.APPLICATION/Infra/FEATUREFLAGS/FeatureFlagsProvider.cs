using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Infra.Context;
using AUTHIO.APPLICATION.Infra.Repositories.BASE;
using Microsoft.EntityFrameworkCore;

namespace AUTHIO.APPLICATION.INFRA.FEATUREFLAGS;

/// <summary>
/// Feature flags provider
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public class FeatureFlagsProvider(
    AuthIoContext context) : GenericEntityCoreRepository<FeatureFlagsEntity>(context), IFeatureFlags
{
    private readonly AuthIoContext _context = context;

    /// <summary>
    /// Recupera uma feature flag
    /// </summary>
    /// <param name="featureName"></param>
    /// <returns></returns>
    public async Task<FeatureFlagsEntity> GetFeatureDefinitionAsync(string featureName)
        => await _context.FeatureFlags.SingleOrDefaultAsync(f => f.Name == featureName);

    /// <summary>
    /// Retorna todas as featuire flags.
    /// </summary>
    /// <returns></returns>
    public IAsyncEnumerable<FeatureFlagsEntity> GetAllFeatureDefinitionsAsync()
        => _context.FeatureFlags.AsAsyncEnumerable();
}
