using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositorio de SendGrid configurations.
/// </summary>
public sealed class SendGridConfigurationRepository(
    AuthIoContext context)
        : GenericEntityCoreRepository<SendGridConfigurationEntity>(context), ISendGridConfigurationRepository
{ }
