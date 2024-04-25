using AUTHIO.DOMAIN.Contracts.Factories;
using AUTHIO.INFRASTRUCTURE.Providers;

namespace AUTHIO.INFRASTRUCTURE.Factories;

/// <summary>
/// Classe de provedor de e-mail.
/// </summary>
public class EmailProviderFactory : IEmailProviderFactory
{
    /// <summary>
    /// Provedor de e-mail do SendGrid.
    /// </summary>
    /// <returns></returns>
    public IEmailProvider GetSendGridEmailProvider()
        => new SendGridEmailProvider();

    /// <summary>
    /// Provedor de e-mail padrão do sistema.
    /// </summary>
    /// <returns></returns>
    public IEmailProvider GetDefaultEmailProvider()
        => new EmailProvider();
}
