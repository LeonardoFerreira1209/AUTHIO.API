using AUTHIO.DATABASE.Context;
using AUTHIO.DATABASE.Repositories.BASE;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AUTHIO.DATABASE.Repositories;

/// <summary>
/// Repositório de usuários.
/// </summary>
public class UserRepository(IOptions<AppSettings> options, SignInManager<UserEntity> signInManager,
    AuthIoContext context,
    UserManager<UserEntity> userManager,
    RoleManager<RoleEntity> roleManager) : BaseRepository(options), IUserRepository
{
    private readonly AuthIoContext _context = context;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly RoleManager<RoleEntity> _roleManager = roleManager;

    /// <summary>
    /// Retorna o resultado de autenticação do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="password"></param>
    /// <param name="isPersistent"></param>
    /// <param name="lockoutOnFailure"></param>
    /// <returns></returns>
    public async Task<SignInResult> PasswordSignInAsync(UserEntity userEntity, string password, bool isPersistent, bool lockoutOnFailure)
        => await _signInManager.PasswordSignInAsync(userEntity, password, isPersistent, lockoutOnFailure);

    /// <summary>
    /// Método responsavel por criar um novo usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<IdentityResult> CreateUserAsync(UserEntity userEntity, string password)
        => await _userManager.CreateAsync(userEntity, password);

    /// <summary>
    /// Método responsavel por atualizar um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    public async Task<IdentityResult> UpdateUserAsync(UserEntity userEntity)
        => await _userManager.UpdateAsync(userEntity);

    /// <summary>
    /// Método responsável por recuperar um usuário.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<UserEntity> GetByIdAsync(Guid userId)
        => await _userManager.Users
                    .FirstAsync(user => user.Id.Equals(userId));

    /// <summary>
    /// Método responsável por recuperar vários usuários por id.
    /// </summary>
    /// <param name="userIds"></param>
    /// <returns></returns>
    public async Task<IEnumerable<UserEntity>> GetByIdsAsync(List<Guid> userIds)
        => await _userManager.Users.Where(user => userIds.Contains(user.Id)).ToListAsync();

    /// <summary>
    /// Método responsável por recuperar vários usuários por nome.
    /// </summary>
    /// <param name="names"></param>
    /// <returns></returns>
    public async Task<IEnumerable<UserEntity>> GetByNamesAsync(List<string> names)
    {
        var normalizedNames
            = names.ConvertAll(name
                => name.RemoveAccentAndConvertToLower());

        var users = await _context.Set<UserEntity>().ToListAsync();

        return users.Where(
            user => normalizedNames.Any(
                normalized =>
                    $"{user.FirstName.RemoveAccentAndConvertToLower()} {user.LastName.RemoveAccentAndConvertToLower()}".Contains(normalized)
                    ||
                normalized.Contains(
                    $"{user.FirstName.RemoveAccentAndConvertToLower()} {user.LastName.RemoveAccentAndConvertToLower()}")
                )
            ||
            names.Any(name => user.Email.Contains(name) || user.UserName.Contains(name)
            ||
            name.Contains(user.Email) || name.Contains(user.UserName))
        ).ToList();
    }

    /// <summary>
    /// Método responsável por recuperar um usuário pelo username.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<UserEntity> GetWithUsernameAsync(string username)
        => await _userManager.FindByNameAsync(username);

    /// <summary>
    /// Método responsável por setar o nome de usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<IdentityResult> SetUserNameAsync(UserEntity userEntity, string username)
        => await _userManager.SetUserNameAsync(userEntity, username);

    /// <summary>
    /// Método responsável por mudar a senha do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="currentPassword"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<IdentityResult> ChangePasswordAsync(UserEntity userEntity, string currentPassword, string password)
        => await _userManager.ChangePasswordAsync(userEntity, currentPassword, password);

    /// <summary>
    /// Método responsável por setar o e-mail do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<IdentityResult> SetEmailAsync(UserEntity userEntity, string email)
        => await _userManager.SetEmailAsync(userEntity, email);

    /// <summary>
    ///  Método responsável por setar o celular do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public async Task<IdentityResult> SetPhoneNumberAsync(UserEntity userEntity, string phoneNumber)
        => await _userManager.SetPhoneNumberAsync(userEntity, phoneNumber);

    /// <summary>
    /// Método responsável por confirmar um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<IdentityResult> ConfirmEmailAsync(UserEntity userEntity, string code)
        => await _userManager.ConfirmEmailAsync(userEntity, code);

    /// <summary>
    /// Método responsável por confirmar um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    public async Task<string> GenerateEmailConfirmationTokenAsync(UserEntity userEntity)
        => await _userManager.GenerateEmailConfirmationTokenAsync(userEntity);

    /// <summary>
    /// Método responsável por adicionar uma claim em um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="claim"></param>
    /// <returns></returns>
    public async Task<IdentityResult> AddClaimUserAsync(UserEntity userEntity, Claim claim)
        => await _userManager.AddClaimAsync(userEntity, claim);

    /// <summary>
    /// Método responsável por remover uma claim em um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="claim"></param>
    /// <returns></returns>
    public async Task<IdentityResult> RemoveClaimUserAsync(UserEntity userEntity, Claim claim)
        => await _userManager.RemoveClaimAsync(userEntity, claim);

    /// <summary>
    /// Método responsável por adicionar uma role em um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task<IdentityResult> AddToUserRoleAsync(UserEntity userEntity, string roleName)
        => await _userManager.AddToRoleAsync(userEntity, roleName);

    /// <summary>
    /// Método responsável por remover uma role em um usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task<IdentityResult> RemoveToUserRoleAsync(UserEntity userEntity, string roleName)
        => await _userManager.RemoveFromRoleAsync(userEntity, roleName);

    /// <summary>
    /// Método responsável por recuperar as roles de usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <returns></returns>
    public async Task<IList<string>> GetUserRolesAsync(UserEntity userEntity)
        => await _userManager.GetRolesAsync(userEntity);

    /// <summary>
    /// Método responsável por recuperar uma role.
    /// </summary>
    /// <param name="roleName"></param>
    /// <returns></returns>
    public async Task<RoleEntity> GetRoleAsync(string roleName)
        => await _roleManager.Roles.FirstOrDefaultAsync(role => role.Name.Equals(roleName));

    /// <summary>
    /// Método responsável por recuperar as claims de uma role.
    /// </summary>
    /// <param name="roleEntity"></param>
    /// <returns></returns>
    public async Task<IList<Claim>> GetRoleClaimsAsync(RoleEntity roleEntity)
        => await _roleManager.GetClaimsAsync(roleEntity);

    /// <summary>
    /// Método responsável por atualizar o token do usuário.
    /// </summary>
    /// <param name="userEntity"></param>
    /// <param name="providerName"></param>
    /// <param name="tokenName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task SetUserAuthenticationTokenAsync(
        UserEntity userEntity, string providerName, string tokenName, string token)
    {
        await _userManager
            .RemoveAuthenticationTokenAsync(
                userEntity, providerName, tokenName).ContinueWith(
                    async (identityResultTask) =>
                    {
                        await _userManager
                            .SetAuthenticationTokenAsync(
                                userEntity, providerName, tokenName, token);
                    });
    }

    ///// <summary>
    ///// Método responsavel por gravar um código de confirmação de usuário.
    ///// </summary>
    ///// <param name="userCodeEntity"></param>
    ///// <returns></returns>
    //public async Task<UserCode> AddUserConfirmationCode(
    //    UserCode userCodeEntity)
    //{
    //    await _context.AddAsync(userCodeEntity);

    //    return userCodeEntity;
    //}

    ///// <summary>
    ///// Método responsavel por atualizar um código de confirmação de usuário.
    ///// </summary>
    ///// <param name="userId"></param>
    ///// <param name="code"></param>
    ///// <returns></returns>
    //public UserCode UpdateUserConfirmationCode(UserCode userCodeEntity)
    //{
    //    _context.Update(userCodeEntity);

    //    return userCodeEntity;
    //}

    ///// <summary>
    /////  Método responsavel por obter os dados de confirmação de usuário.
    ///// </summary>
    ///// <param name="userId"></param>
    ///// <param name="code"></param>
    ///// <returns></returns>
    //public async Task<UserCode> GetUserConfirmationCode(
    //    Guid userId, string code)
    //    => await _context.AspNetUserCodes.FirstOrDefaultAsync(
    //            x => x.UserId.Equals(userId) && x.NumberCode.Equals(code));
}
