using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.DOMAIN.BUILDERS;

namespace AUTHIO.APPLICATION.Domain.Builders;

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
    /// Constroi um usuário de tenants.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static UserEntity CreateUserTenant(this RegisterUserRequest registerUserRequest, Guid tenantId)
       => new UserBuilder()
           .AddFirstName(registerUserRequest.FirstName)
               .AddUserName(registerUserRequest.UserName)
                   .AddLastName(registerUserRequest.LastName)
                       .AddEmail(registerUserRequest.Email)
                           .AddPhoneNumber(registerUserRequest.PhoneNumber)
                               .AddCreated(DateTime.Now)
                                   .AddEmailConfirmed(true)
                                       .AddStatus(Status.Ativo)
                                          .AddTenantId(tenantId)
                                             .Builder();
}
