using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.INFRASTRUCTURE.Context;
using AUTHIO.INFRASTRUCTURE.Repositories.Base;

namespace AUTHIO.INFRASTRUCTURE.Repositories;

/// <summary>
/// Repositorio de SendGrid configurations.
/// </summary>
public sealed class SendGridConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<SendGridConfigurationEntity>(context), ISendGridConfigurationRepository
{ }
