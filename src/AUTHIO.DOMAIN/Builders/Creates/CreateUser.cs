using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;

namespace AUTHIO.DOMAIN.Builders.Creates;

/// <summary>
/// Construtor de usuários.
/// </summary>
public static class CreateUser
{
    /// <summary>
    /// Constroi usuário do systema.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <returns></returns>
    public static UserEntity CreateUserSystem(this RegisterUserRequest registerUserRequest)
        => new UserBuilder()
            .AddFirstName(registerUserRequest.FirstName)
            .AddUserName(registerUserRequest.UserName)
            .AddLastName(registerUserRequest.LastName)
            .AddEmail(registerUserRequest.Email)
            .AddPhoneNumber(registerUserRequest.PhoneNumber)
            .AddCreated(DateTime.Now)
            .AddEmailConfirmed(true)
            .AddStatus(Status.Ativo)
            .AddSystem(true)
            .Builder();

    /// <summary>
    /// Constroi um usuário de Clients.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public static UserEntity CreateUserClient(this RegisterUserRequest registerUserRequest, Guid clientId)
       => new UserBuilder()
            .AddFirstName(registerUserRequest.FirstName)
            .AddUserName(registerUserRequest.UserName)
            .AddLastName(registerUserRequest.LastName)
            .AddEmail(registerUserRequest.Email)
            .AddPhoneNumber(registerUserRequest.PhoneNumber)
            .AddCreated(DateTime.Now)
            .AddEmailConfirmed(true)
            .AddStatus(Status.Ativo)
            .AddClientId(clientId)
            .Builder();
}
