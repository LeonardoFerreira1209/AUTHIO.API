using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Interface de Repositorio de planos.
/// </summary>
public interface IPlanRepository
    : IGenerictEntityCoreRepository<PlanEntity>
{ }
