using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de planos.
/// </summary>
public sealed class PlanRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<PlanEntity>(context), IPlanRepository
{ }
