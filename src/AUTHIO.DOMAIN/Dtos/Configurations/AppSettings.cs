namespace AUTHIO.DOMAIN.Dtos.Configurations;

/// <summary>
/// Classe responsavel por receber os dados do Appsettings.
/// </summary>
public sealed class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public SwaggerInfo SwaggerInfo { get; set; }
    public RetryPolicy RetryPolicy { get; set; }
    public Auth Auth { get; set; }
    public Email Email { get; set; }
    public ServiceBus ServiceBus { get; set; }
    public Hangfire Hangfire { get; set; }
}

/// <summary>
/// Classe responsável por receber dados de retry policy.
/// </summary>
public sealed class RetryPolicy
{
    public string RetryOn { get; set; }
    public int RetryCount { get; set; }
    public int RetryEachSecond { get; set; }
}

/// <summary>
/// Classe de conexões.
/// </summary>
public sealed class ConnectionStrings
{
    public string DataBase { get; set; }
    public string AzureBlobStorage { get; set; }
}

/// <summary>
/// Classe de config do swagger.
/// </summary>
public sealed class SwaggerInfo
{
    public string ApiDescription { get; set; }
    public string ApiVersion { get; set; }
    public string UriMyGit { get; set; }
}

/// <summary>
/// Classe de config de autenticação.
/// </summary>
public sealed class Auth
{
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public string SecurityKey { get; set; }
    public int ExpiresIn { get; set; }
    public Password Password { get; set; }
}

/// <summary>
/// Classe de config de senha.
/// </summary>
public sealed class Password
{
    public int RequiredLength { get; set; }
}

/// <summary>
/// Classe de config de e-mail
/// </summary>
public sealed class Email
{
    /// <summary>
    /// Config de SendGrid.
    /// </summary>
    public SendGrid SendGrid { get; set; }
}

/// <summary>
/// Classe de config do sendGrid.
/// </summary>
public sealed class SendGrid
{
    /// <summary>
    /// Chave de api do sendGrid.
    /// </summary>
    public string ApiKey { get; set; }
}

/// <summary>
/// Classe de config do service bus.
/// </summary>
public sealed class ServiceBus
{
    /// <summary>
    /// String de conexão do service bus.
    /// </summary>
    public string ConnectionString { get; set; }
}

/// <summary>
/// Classe de config de hangfire.
/// </summary>
public sealed class Hangfire
{
    /// <summary>
    /// Classe de job.
    /// </summary>
    public sealed class JobInfo
    {
        /// <summary>
        /// Nome do job.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rotina de execução.
        /// </summary>
        public string Cronn { get; set; }

        /// <summary>
        /// Execução habilitada.
        /// </summary>
        public bool Execute { get; set; }
    }

    /// <summary>
    /// Jobs que serão executados.
    /// </summary>
    public List<JobInfo> Jobs { get; set; }
}