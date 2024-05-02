using AUTHIO.DOMAIN.Contracts.Providers;

namespace AUTHIO.DOMAIN.Contracts.Factories;

/// <summary>
/// Fabrica de provedores de e-mail.
/// </summary>
public interface IEmailProviderFactory
{
    /// <summary>
    /// Provedor de e-mail do SendGrid.
    /// </summary>
    /// <returns></returns>
    public IEmailProvider GetSendGridEmailProvider();

    /// <summary>
    /// Provedor de e-mail padrão do sistema.
    /// </summary>
    /// <returns></returns>
    public IEmailProvider GetDefaultEmailProvider();
}
