using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST.SYSTEM;

namespace AUTHIO.APPLICATION.Domain.Utils.Extensions;

/// <summary>
/// Classe de extensão de usuários;
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Converte uma request em uma entidade.
    /// </summary>
    /// <param name="registerSystemUserRequest"></param>
    /// <returns></returns>
    public static UserEntity ToEntity(this RegisterUserRequest registerUserRequest)
        => new()
        {
            FirstName = registerUserRequest.FirstName,
            LastName = registerUserRequest.LastName,
            Email = registerUserRequest.Email,
            Created = DateTime.Now,
            Status = Status.Ativo,
            UserName = registerUserRequest.UserName,
            System = true
        };

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
