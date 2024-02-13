using AUTHIO.APPLICATION.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AUTHIO.APPLICATION.DOMAIN.CONTRACTS.REPOSITORY;

/// <summary>
/// Interface de repositório de usuários.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retorna o resultado de autenicação do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="password"></param>
    /// <param name="isPersistent"></param>
    /// <param name="lockoutOnFailure"></param>
    /// <returns></returns>
    Task<SignInResult> PasswordSignInAsync(UserEntity userEntity, string password, bool isPersistent, bool lockoutOnFailure);

    /// <summary>
    /// Método responsavel por criar um novo usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<IdentityResult> CreateUserAsync(UserEntity userEntity, string password);

    /// <summary>
    /// Método responsavel por atualizar um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    Task<IdentityResult> UpdateUserAsync(UserEntity userEntity);

    /// <summary>
    /// Método responsável por recuperar um usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<UserEntity> GetByIdAsync(Guid userId);

    /// <summary>
    /// Método responsável por recuperar vários usuários por id.
    /// </summary>
    /// <param name="userIds"></param>
    /// <returns></returns>
    Task<IEnumerable<UserEntity>> GetByIdsAsync(List<Guid> userIds);

    /// <summary>
    /// Método responsável por recuperar vários usuários por nome.
    /// </summary>
    /// <param name="names"></param>
    /// <returns></returns>
    Task<IEnumerable<UserEntity>> GetByNamesAsync(List<string> names);

    /// <summary>
    /// Método responsável por recuperar um usuário pelo username.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<UserEntity> GetWithUsernameAsync(string username);

    /// <summary>
    /// Método responsável por setar o nome de usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    Task<IdentityResult> SetUserNameAsync(UserEntity userEntity, string username);

    /// <summary>
    /// Método responsável por mudar a senha do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="currentPassword"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<IdentityResult> ChangePasswordAsync(UserEntity userEntity, string currentPassword, string password);

    /// <summary>
    /// Método responsável por setar o e-mail do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<IdentityResult> SetEmailAsync(UserEntity userEntity, string email);

    /// <summary>
    ///  Método responsável por setar o celular do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    Task<IdentityResult> SetPhoneNumberAsync(UserEntity userEntity, string phoneNumber);

    /// <summary>
    /// Método responsável por gerar uma código de confirmação de usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<IdentityResult> ConfirmEmailAsync(UserEntity userEntity, string code);

    /// <summary>
    /// Método responsável por confirmar um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    Task<string> GenerateEmailConfirmationTokenAsync(UserEntity userEntity);

    /// <summary>
    /// Método responsável por adicionar uma claim em um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="claim"></param>
    /// <returns></returns>
    Task<IdentityResult> AddClaimUserAsync(UserEntity userEntity, Claim claim);

    /// <summary>
    /// Método responsável por remover uma claim em um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="claim"></param>
    /// <returns></returns>
    Task<IdentityResult> RemoveClaimUserAsync(UserEntity userEntity, Claim claim);

    /// <summary>
    /// Método responsável por adicionar uma role em um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    Task<IdentityResult> AddToUserRoleAsync(UserEntity userEntity, string roleName);

    /// <summary>
    /// Método responsável por remover uma role em um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    Task<IdentityResult> RemoveToUserRoleAsync(UserEntity userEntity, string roleName);

    /// <summary>
    /// Método responsável por recuperar as roles de usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    Task<IList<string>> GetUserRolesAsync(UserEntity userEntity);

    /// <summary>
    /// Método responsável por recuperar uma role.
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    Task<RoleEntity> GetRoleAsync(string roleName);

    /// <summary>
    /// Método responsável por recuperar as claims de uma role.
    /// </summary>
    /// <param name="roleEntity"></param>
    /// <returns></returns>
    Task<IList<Claim>> GetRoleClaimsAsync(RoleEntity roleEntity);

    /// <summary>
    ///  Método responsável por setar um token no usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="providerName"></param>
    /// <param name="tokenName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task SetUserAuthenticationTokenAsync(UserEntity userEntity, string providerName, string tokenName, string token);

    ///// <summary>
    ///// Método responsavel por gravar um código de confirmação de usuário.
    ///// </summary>
    ///// <param name="userCodeEntity"></param>
    ///// <returns></returns>
    //Task<UserCode> AddUserConfirmationCode(UserCode userCodeEntity);

    ///// <summary>
    ///// Método responsavel por atualizar um código de confirmação de usuário.
    ///// </summary>
    ///// <param name="userCodeEntity"></param>
    ///// <returns></returns>
    //UserCode UpdateUserConfirmationCode(UserCode userCodeEntity);

    ///// <summary>
    /////  Método responsavel por obter os dados de confirmação de usuário.
    ///// </summary>
    ///// <param name="userId"></param>
    ///// <param name="code"></param>
    ///// <returns></returns>
    //Task<UserCode> GetUserConfirmationCode(Guid userId, string code);
}