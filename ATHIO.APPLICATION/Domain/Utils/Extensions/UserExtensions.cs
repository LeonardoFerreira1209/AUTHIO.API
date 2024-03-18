using AUTHIO.APPLICATION.Domain.Builders;
using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Utils.Extensions;

/// <summary>
/// Classe de extensão de usuários;
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Converte uma request em uma entidade system.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <returns></returns>
    public static UserEntity ToUserSystemEntity(this RegisterUserRequest registerUserRequest)
        => CreateUser.CreateUserSystem(
            registerUserRequest);

    /// <summary>
    /// Converte uma request em uma entidade user tenant.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static UserEntity ToUserTenantEntity(this RegisterUserRequest registerUserRequest, Guid tenantId)
        => CreateUser.CreateUserTenant(
            registerUserRequest, tenantId);

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
