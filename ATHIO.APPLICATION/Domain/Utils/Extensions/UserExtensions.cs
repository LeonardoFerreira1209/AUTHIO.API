using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.DOMAIN.BUILDERS;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST.SYSTEM;

namespace AUTHIO.APPLICATION.Domain.Utils.Extensions;

/// <summary>
/// Classe de extensão de usuários;
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Converte uma request em uma entidade system.
    /// </summary>
    /// <param name="registerSystemUserRequest"></param>
    /// <returns></returns>
    public static UserEntity ToUserSystemEntity(this RegisterUserRequest registerUserRequest)
        => new UserSystemBuilder()
            .AddFirstName(registerUserRequest.FirstName)
                .AddUserName(registerUserRequest.UserName)
                    .AddLastName(registerUserRequest.LastName)
                        .AddEmail(registerUserRequest.Email)
                            .AddPhoneNumber(registerUserRequest.PhoneNumber)
                                .AddCreated(DateTime.Now)
                                    .AddEmailConfirmed(false)
                                        .AddStatus(Status.Ativo)
                                            .Builder();

    /// <summary>
    /// Converte uma request em uma entidade user tenant.
    /// </summary>
    /// <param name="registerSystemUserRequest"></param>
    /// <returns></returns>
    public static UserEntity ToUserTenantEntity(this RegisterTenantUserRequest registerTenantUserRequest, Guid tenantId)
        => new UserTenantBuilder()
            .AddFirstName(registerTenantUserRequest.FirstName)
                .AddUserName(registerTenantUserRequest.UserName)
                    .AddLastName(registerTenantUserRequest.LastName)
                        .AddEmail(registerTenantUserRequest.Email)
                            .AddPhoneNumber(registerTenantUserRequest.PhoneNumber)
                                .AddCreated(DateTime.Now)
                                    .AddEmailConfirmed(true)
                                        .AddStatus(Status.Ativo)
                                           .AddTenantId(tenantId)
                                              .Builder();

    /// <summary>
    /// Converte uma entidade em um Response.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    public static UserResponse ToResponse(this UserEntity userEntity)
        => new()
        {
            Id = userEntity.Id,
            Name = userEntity.FirstName,
            LastName = userEntity.LastName,
            Email = userEntity.Email,
            Created = userEntity.Created,
            Updated = userEntity.Updated,
            Status = userEntity.Status,
            TenantId = userEntity.TenantId,
            UserName = userEntity.UserName,
            System = userEntity.System
        };
}
