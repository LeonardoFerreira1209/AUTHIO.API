using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Builders;

/// <summary>
/// Classe de builder de Password identity configurations.
/// </summary>
public sealed class PasswordIdentityConfigurationBuilder
{
    private Guid tenantIdentityConfigurationId;
    private DateTime? updated = null;
    private DateTime created;
    private int requiredLenght;
    private int requiredUniqueChars;
    private bool requireNonAlphanumeric;
    private bool requireLowercase;
    private bool requireUppercase;
    private bool requireDigit;

    /// <summary>
    /// Adiciona a quantidade requerido de caracters na senha.
    /// </summary>
    /// <param name="requiredLength"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddRequiredLength(int requiredLength)
    {
        requiredLenght = requiredLength;

        return this;
    }

    /// <summary>
    /// Adiciona a quantidade requerido de char unicos na senha.
    /// </summary>
    /// <param name="requiredUniqueChars"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddRequiredUniqueChars(int requiredUniqueChars)
    {
        this.requiredUniqueChars = requiredUniqueChars;

        return this;
    }

    /// <summary>
    /// Adiciona se deve requirir não alfanumericos na senha.
    /// </summary>
    /// <param name="requireNonAlphanumeric"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddRequireNonAlphanumeric(bool requireNonAlphanumeric)
    {
        this.requireNonAlphanumeric = requireNonAlphanumeric;

        return this;
    }

    /// <summary>
    /// Adiciona se deve requirir letras minusculas na senha.
    /// </summary>
    /// <param name="requireLowercase"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddRequireLowercase(bool requireLowercase)
    {
        this.requireLowercase = requireLowercase;

        return this;
    }

    /// <summary>
    /// Adiciona se deve requirir letras maiusculas na senha.
    /// </summary>
    /// <param name="requireUppercase"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddRequireUppercase(bool requireUppercase)
    {
        this.requireUppercase = requireUppercase;

        return this;
    }

    /// <summary>
    /// Adiciona se deve requirir digito na senha.
    /// </summary>
    /// <param name="requireDigit"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddRequireDigit(bool requireDigit)
    {
        this.requireDigit = requireDigit;

        return this;
    }

    /// <summary>
    /// Adiciona um TenantIdentityConfigurationId.
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddTenantIdentityConfigurationId(Guid tenantIdentityConfigurationId)
    {
        this.tenantIdentityConfigurationId = tenantIdentityConfigurationId;

        return this;
    }

    /// <summary>
    /// Adiciona a data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddCreated(DateTime? created = null)
    {
        this.created
            = created
            ?? DateTime.Now;

        return this;
    }


    /// <summary>
    /// Adiciona a data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    public PasswordIdentityConfigurationBuilder AddUpdated(DateTime? updated = null)
    {
        this.updated
           = updated
           ?? DateTime.Now;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public PasswordIdentityConfigurationEntity Builder()
        => new(tenantIdentityConfigurationId, created,
            updated, requiredLenght, requireDigit, requiredUniqueChars,
            requireNonAlphanumeric, requireLowercase, requireUppercase);
}
