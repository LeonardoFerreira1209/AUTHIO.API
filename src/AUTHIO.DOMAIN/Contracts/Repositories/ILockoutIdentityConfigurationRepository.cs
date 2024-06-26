﻿using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Contracts.Repositories;

/// <summary>
/// Repositorio de Lockout identity configurations.
/// </summary>
public interface ILockoutIdentityConfigurationRepository
    : IGenerictEntityCoreRepository<LockoutIdentityConfigurationEntity>
{ }
