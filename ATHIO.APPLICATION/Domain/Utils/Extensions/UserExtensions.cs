using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Entity;

namespace AUTHIO.APPLICATION.Domain.Utils.Extensions;

/// <summary>
/// Classe de extensão de usuários;
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Converte uma entidade em um Response.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    public static UserResponse ToResponse(this UserEntity userEntity)
        => new()
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            LastName = userEntity.LastName,
            Email = userEntity.Email,
            Created = userEntity.Created,
            Updated = userEntity.Updated,
            Status = userEntity.Status,
            TenantId = userEntity.TenantId,
            UserName = userEntity.UserName,
        };
}
