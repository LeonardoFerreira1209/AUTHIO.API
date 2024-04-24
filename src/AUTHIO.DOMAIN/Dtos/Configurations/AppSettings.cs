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
    public Mail Mail { get; set; }
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
public class SwaggerInfo
{
    public string ApiDescription { get; set; }
    public string ApiVersion { get; set; }
    public string UriMyGit { get; set; }
}

/// <summary>
/// Classe de config de autenticação.
/// </summary>
public class Auth
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
public sealed class Mail
{
    /// <summary>
    /// Chave da API.
    /// </summary>
    public string TenantKey { get; set; }
}
