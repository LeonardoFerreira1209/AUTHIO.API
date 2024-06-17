﻿using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositorio de Tenant token configurations.
/// </summary>
public sealed class TenantTokenConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<TenantTokenConfigurationEntity>(context), ITenantTokenConfigurationRepository
{ }
