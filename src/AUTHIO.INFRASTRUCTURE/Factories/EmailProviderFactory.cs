using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.DOMAIN.Contracts.Providers.Email;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.INFRASTRUCTURE.Providers;
using Microsoft.Extensions.Options;

namespace AUTHIO.INFRASTRUCTURE.Factories;

/// <summary>
/// Classe de provedor de e-mail.
/// </summary>
public sealed class EmailProviderFactory(
    IOptions<AppSettings> appSettings) : IEmailProviderFactory
{
    /// <summary>
    /// Provedor de e-mail do SendGrid.
    /// </summary>
    /// <returns></returns>
    public IEmailProvider GetSendGridEmailProvider()
        => new SendGridEmailProvider(appSettings);

    /// <summary>
    /// Provedor de e-mail padrão do sistema.
    /// </summary>
    /// <returns></returns>
    public IEmailProvider GetDefaultEmailProvider()
        => new EmailProvider();
}
