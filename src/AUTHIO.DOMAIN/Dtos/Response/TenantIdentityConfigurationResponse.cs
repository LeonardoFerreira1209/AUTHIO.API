﻿namespace AUTHIO.DOMAIN.Dtos.Response;

/// <summary>
/// Classe de response de TenantIdentityConfiguration.
/// </summary>
public class TenantIdentityConfigurationResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Data de criação.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Data de atualização.
    /// </summary>
    public DateTime? Updated { get; set; }

    /// <summary>
    /// Id do tenant configuration Id.
    /// </summary>
    public Guid TenantConfigurationId { get; set; }

    /// <summary>
    /// Entidade do tenant configuration.
    /// </summary>
    public virtual TenantConfigurationResponse TenantConfiguration { get; set; }

    /// <summary>
    /// Entidade de user identity configuration.
    /// </summary>
    public virtual UserIdentityConfigurationResponse UserIdentityConfiguration { get; set; }

    /// <summary>
    /// Entidade de password identity configuration.
    /// </summary>
    public virtual PasswordIdentityConfigurationResponse PasswordIdentityConfiguration { get; set; }

    /// <summary>
    /// Entidade de lockout identity configuration.
    /// </summary>
    public virtual LockoutIdentityConfigurationResponse LockoutIdentityConfiguration { get; set; }
}
