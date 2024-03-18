using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.DOMAIN.BUILDERS;

/// <summary>
/// Interface de User Builder.
/// </summary>
/// <typeparam name="UserBuilder"></typeparam>
/// <typeparam name="Y"></typeparam>
public interface IUserBuilder
{
    /// <summary>
    /// Adiciona o nome.
    /// </summary>
    /// <param name="firstName"></param>
    /// <returns></returns>
    UserBuilder AddFirstName(string firstName);

    /// <summary>
    /// Adiciona sobrenonme.
    /// </summary>
    /// <param name="lastName"></param>
    /// <returns></returns>
    UserBuilder AddLastName(string lastName);

    /// <summary>
    /// Adiciona username.
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    UserBuilder AddUserName(string userName);

    /// <summary>
    /// Adiciona data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    UserBuilder AddCreated(DateTime created);

    /// <summary>
    /// Adiciona data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    UserBuilder AddUpdated(DateTime updated);

    /// <summary>
    /// Adiciona status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    UserBuilder AddStatus(Status status);

    /// <summary>
    /// Adiciona numero de telefone.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    UserBuilder AddPhoneNumber(string phoneNumber);

    /// <summary>
    /// Adiciona o e-mail.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    UserBuilder AddEmail(string email);

    /// <summary>
    /// Adiciona e-mail confirmado.
    /// </summary>
    /// <param name="emailConfirmed"></param>
    /// <returns></returns>
    UserBuilder AddEmailConfirmed(bool emailConfirmed);

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public UserEntity Builder();
}
