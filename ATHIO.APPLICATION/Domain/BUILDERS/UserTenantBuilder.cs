using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.DOMAIN.BUILDERS;

/// <summary>
/// Classe responsavel por construir um usuário do systema.
/// </summary>
public class UserTenantBuilder
    : IUserBuilder<UserTenantBuilder, UserEntity>
{
    private string firstName, userName, lastName, phoneNumber, email;
    private DateTime created;
    private DateTime? updated;
    private Status status;
    private bool emailConfirmed;
    private Guid tenantId;

    /// <summary>
    /// Adiciona o nome.
    /// </summary>
    /// <param name="firstName"></param>
    /// <returns></returns>
    public UserTenantBuilder AddFirstName(string firstName)
    {
        this.firstName = firstName;

        return this;
    }

    /// <summary>
    /// Adiciona sobrenonme.
    /// </summary>
    /// <param name="lastName"></param>
    /// <returns></returns>
    public UserTenantBuilder AddLastName(string lastName)
    {
        this.lastName = lastName;

        return this;
    }

    /// <summary>
    /// Adiciona username.
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public UserTenantBuilder AddUserName(string userName)
    {
        this.userName = userName;

        return this;
    }

    /// <summary>
    /// Adiciona data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public UserTenantBuilder AddCreated(DateTime created)
    {
        this.created = created;

        return this;
    }

    /// <summary>
    /// Adiciona data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    public UserTenantBuilder AddUpdated(DateTime updated)
    {
        this.updated = updated;

        return this;
    }

    /// <summary>
    /// Adiciona status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public UserTenantBuilder AddStatus(Status status)
    {
        this.status = status;

        return this;
    }

    /// <summary>
    /// Adiciona numero de telefone.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public UserTenantBuilder AddPhoneNumber(string phoneNumber)
    {
        this.phoneNumber = phoneNumber;

        return this;
    }


    /// <summary>
    /// Adiciona o e-mail.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public UserTenantBuilder AddEmail(string email)
    {
        this.email = email;

        return this;
    }

    /// <summary>
    /// Adiciona e-mail confirmado.
    /// </summary>
    /// <param name="emailConfirmed"></param>
    /// <returns></returns>
    public UserTenantBuilder AddEmailConfirmed(bool emailConfirmed)
    {
        this.emailConfirmed = emailConfirmed;

        return this;
    }

    /// <summary>
    /// Adiciona o tenantId.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public UserTenantBuilder AddTenantId(Guid tenantId)
    {
        this.tenantId = tenantId;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public UserEntity Builder() => new(
        firstName, lastName, userName,
        email, phoneNumber, status, created, emailConfirmed, updated, tenantId);
}
