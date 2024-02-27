using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.DOMAIN.BUILDERS;

/// <summary>
/// Interface de User Builder.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="Y"></typeparam>
public interface IUserBuilder<T, Y> 
    where T : class 
    where Y : IEntityBase
{
    /// <summary>
    /// Adiciona o nome.
    /// </summary>
    /// <param name="firstName"></param>
    /// <returns></returns>
    T AddFirstName(string firstName);

    /// <summary>
    /// Adiciona sobrenonme.
    /// </summary>
    /// <param name="lastName"></param>
    /// <returns></returns>
    T AddLastName(string lastName);

    /// <summary>
    /// Adiciona username.
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    T AddUserName(string userName);

    /// <summary>
    /// Adiciona data de criação.
    /// </summary>
    /// <param name="created"></param>
    /// <returns></returns>
    T AddCreated(DateTime created);

    /// <summary>
    /// Adiciona data de atualização.
    /// </summary>
    /// <param name="updated"></param>
    /// <returns></returns>
    T AddUpdated(DateTime updated);

    /// <summary>
    /// Adiciona status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    T AddStatus(Status status);

    /// <summary>
    /// Adiciona numero de telefone.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    T AddPhoneNumber(string phoneNumber);

    /// <summary>
    /// Adiciona o e-mail.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    T AddEmail(string email);

    /// <summary>
    /// Adiciona e-mail confirmado.
    /// </summary>
    /// <param name="emailConfirmed"></param>
    /// <returns></returns>
    T AddEmailConfirmed(bool emailConfirmed);

    /// <summary>
    /// Cria a entidade.
    /// </summary>
    /// <returns></returns>
    public Y Builder();
}
