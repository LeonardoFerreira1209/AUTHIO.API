using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.DOMAIN.BUILDERS;

/// <summary>
/// Classe responsavel por construir um usuário do systema.
/// </summary>
public class UserBuilder
{
    private string firstName, userName, lastName, phoneNumber, email;
    private DateTime created;
    private DateTime? updated;
    private Status status;
    private bool emailConfirmed;
    private Guid? tenantId;
    private bool system;

    /// <summary>
    /// Adiciona o nome.
    /// </summary>
    /// <param name="firstName"></param>
    /// <returns></returns>
    public UserBuilder AddFirstName(string firstName)
    {
        this.firstName = firstName;

        return this;
    }

    /// <summary>
    /// Adiciona sobrenonme.
    /// </summary>
    /// <param name="lastName"></param>
    /// <returns></returns>
    public UserBuilder AddLastName(string lastName)
    {
        this.lastName = lastName;

        return this;
    }

    /// <summary>
    /// Adiciona username.
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public UserBuilder AddUserName(string userName)
    {
        this.userName = userName;

        return this;
    }

    /// <summary>
    /// Adiciona data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    public UserBuilder AddCreated(DateTime created)
    {
        this.created = created;

        return this;
    }

    /// <summary>
    /// Adiciona data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    public UserBuilder AddUpdated(DateTime updated)
    {
        this.updated = updated;

        return this;
    }

    /// <summary>
    /// Adiciona status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public UserBuilder AddStatus(Status status)
    {
        this.status = status;

        return this;
    }

    /// <summary>
    /// Adiciona numero de telefone.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public UserBuilder AddPhoneNumber(string phoneNumber)
    {
        this.phoneNumber = phoneNumber;

        return this;
    }


    /// <summary>
    /// Adiciona o e-mail.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public UserBuilder AddEmail(string email)
    {
        this.email = email;

        return this;
    }

    /// <summary>
    /// Adiciona e-mail confirmado.
    /// </summary>
    /// <param name="emailConfirmed"></param>
    /// <returns></returns>
    public UserBuilder AddEmailConfirmed(bool emailConfirmed)
    {
        this.emailConfirmed = emailConfirmed;

        return this;
    }

    /// <summary>
    /// Adiciona o tenantId.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public UserBuilder AddTenantId(Guid tenantId)
    {
        this.tenantId = tenantId;

        return this;
    }

    /// <summary>
    /// Adiciona o system.
    /// </summary>
    /// <param name="system"></param>
    /// <returns></returns>
    public UserBuilder AddSystem(bool system)
    {
        this.system = system;

        return this;
    }

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public UserEntity Builder() => new(
        firstName, lastName, userName,
        email, phoneNumber, status, created, emailConfirmed, updated, tenantId, system);
}
