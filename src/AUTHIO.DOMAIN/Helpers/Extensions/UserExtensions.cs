using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Entities;
using MySqlX.XDevAPI;

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
        => registerUserRequest.CreateUserSystem();

    /// <summary>
    /// Converte uma request em uma entidade user Client.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public static UserEntity ToUserClientEntity(
            this RegisterUserRequest registerUserRequest, 
            Guid clientId
    )
        => registerUserRequest.CreateUserClient(clientId);

    /// <summary>
    /// Atualiza uma entidade baseada em request.
    /// </summary>
    /// <param name="updateUserRequest"></param>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    public static UserEntity UpdateEntity(
        this UserEntity userEntity,
        UpdateUserRequest updateUserRequest
        )
    {
        userEntity.FirstName = updateUserRequest.FirstName;
        userEntity.LastName = updateUserRequest.LastName;
        userEntity.Email = updateUserRequest.Email;
        userEntity.Status = updateUserRequest.Status;
        userEntity.Updated = DateTime.Now;

        return userEntity;
    }

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
            ClientId = userEntity.ClientId,
            UserName = userEntity.UserName,
            System = userEntity.System,
            Subscription = includeSubscription 
                ? userEntity.Subscription?.ToResponse(true, false) 
                : null
        };
}
