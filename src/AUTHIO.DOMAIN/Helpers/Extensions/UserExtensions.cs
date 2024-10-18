using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

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
    public static UserEntity ToUserSystemEntity(
            this RegisterUserRequest registerUserRequest
        )
        => registerUserRequest.CreateUserSystem(
);

    /// <summary>
    /// Converte uma request em uma entidade user tenant.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static UserEntity ToUserTenantEntity(
            this RegisterUserRequest registerUserRequest, 
            Guid tenantId
        )
        => registerUserRequest.CreateUserTenant(tenantId);

    /// <summary>
    /// Converte uma entidade em um Response.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    public static UserResponse ToResponse(
            this UserEntity userEntity, 
            bool includeSubscription = true
        )
        => new()
        {
            Id = userEntity.Id,
            Name = userEntity.FirstName,
            LastName = userEntity.LastName,
            SubscriptionId = userEntity.SubscriptionId,
            Email = userEntity.Email,
            Created = userEntity.Created,
            Updated = userEntity.Updated,
            Status = userEntity.Status,
            TenantId = userEntity.TenantId,
            UserName = userEntity.UserName,
            System = userEntity.System,
            Subscription = includeSubscription 
                ? userEntity.Subscription?.ToResponse(true, false) 
                : null
        };
}
