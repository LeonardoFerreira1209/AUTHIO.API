using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Feature flags provider
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public class FeatureFlagsRepository(
    AuthIoContext context) : GenericEntityCoreRepository<FeatureFlagsEntity>(context), IFeatureFlagsRepository
{
    private readonly AuthIoContext _context = context;

    /// <summary>
    /// Recupera uma feature flag
    /// </summary>
    /// <param name="featureName"></param>
    /// <returns></returns>
    public async Task<FeatureFlagsEntity> GetFeatureDefinitionAsync(
        string featureName, 
        CancellationToken cancellationToken
        )
        => await _context.FeatureFlags
            .SingleOrDefaultAsync(
                f => f.Name == featureName, 
                cancellationToken
            );

    /// <summary>
    /// Retorna todas as featuire flags.
    /// </summary>
    /// <returns></returns>
    public IAsyncEnumerable<FeatureFlagsEntity> GetAllFeatureDefinitionsAsync()
        => _context.FeatureFlags.AsAsyncEnumerable();
}
