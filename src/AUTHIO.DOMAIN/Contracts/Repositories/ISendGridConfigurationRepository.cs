using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de SendGrid configurations.
/// </summary>
public interface ISendGridConfigurationRepository
    : IGenerictEntityCoreRepository<SendGridConfigurationEntity>
{ }
