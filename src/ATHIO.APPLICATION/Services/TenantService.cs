using AUTHIO.DATABASE.Context;
using AUTHIO.DOMAIN.Builders.Creates;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Exceptions;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.DOMAIN.Validators;
using AUTHIO.INFRASTRUCTURE.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static AUTHIO.DOMAIN.Exceptions.CustomTenantExceptions;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.Services;

/// <summary>
/// Classe de serviço de Tenants.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public class TenantService(
    IUnitOfWork<AuthIoContext> unitOfWork,
    ITenantRepository tenantRepository,
    ITenantConfigurationRepository tenantConfigurationRepository,
    ITenantIdentityConfigurationRepository tenantIdentityConfigurationRepository,
    IUserIdentityConfigurationRepository userIdentityConfigurationRepository,
    IPasswordIdentityConfigurationRepository passwordIdentityConfigurationRepository,
    ILockoutIdentityConfigurationRepository lockoutIdentityConfigurationRepository,
    IContextService contextService,
    CustomUserManager<UserEntity> customUserManager) : ITenantService
{
    private readonly IUnitOfWork<AuthIoContext>
       _unitOfWork = unitOfWork;

    private readonly ITenantRepository
        _tenantRepository = tenantRepository;

    private readonly ITenantConfigurationRepository
        _tenantConfigurationRepository = tenantConfigurationRepository;

    private readonly ITenantIdentityConfigurationRepository
        _tenantIdentityConfigurationRepository = tenantIdentityConfigurationRepository;

    private readonly IUserIdentityConfigurationRepository
        _userIdentityConfigurationRepository = userIdentityConfigurationRepository;

    private readonly IPasswordIdentityConfigurationRepository
        _passwordIdentityConfigurationRepository = passwordIdentityConfigurationRepository;

    private readonly ILockoutIdentityConfigurationRepository
        _lockoutIdentityConfigurationRepository = lockoutIdentityConfigurationRepository;

    private readonly IContextService
        _contextService = contextService;

    private readonly CustomUserManager<UserEntity>
        _customUserManager = customUserManager;

    /// <summary>
    /// Método responsável por criar um Tenant.
    /// </summary>
    /// <param name="createTenantRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="DuplicatedTenantException"></exception>
    public async Task<ObjectResult> CreateAsync(
        CreateTenantRequest createTenantRequest, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(TenantService)} - METHOD {nameof(CreateAsync)}\n");

        try
        {
            await new CreateTenantRequestValidator()
                .ValidateAsync(createTenantRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false)
                            await validation.GetValidationErrors();

                    }).Unwrap();

            return await _tenantRepository.GetAsync(tenant => tenant.Name == createTenantRequest.Name)
                .ContinueWith(async (tenantResult) =>
                {
                    if (tenantResult.Result is not null)
                        throw new DuplicatedTenantException(createTenantRequest);

                    var _transaction
                        = await _unitOfWork.BeginTransactAsync();

                    try
                    {
                        return await _tenantRepository.CreateAsync(
                           createTenantRequest.ToEntity(_contextService.GetCurrentUserId()))
                               .ContinueWith(async (tenantEntityTask) =>
                               {
                                   var tenant
                                       = tenantEntityTask.Result;

                                   await _tenantConfigurationRepository.CreateAsync(
                                        CreateTenantConfiguration.CreateDefault(
                                            tenant.Id)).ContinueWith(
                                               async (tenantConfigurationEntityTask) =>
                                               {
                                                   var tenantConfiguration
                                                        = tenantConfigurationEntityTask.Result;

                                                   await _tenantIdentityConfigurationRepository.CreateAsync(
                                                       CreateTenantIdentityConfiguration.CreateDefault(
                                                                tenantConfiguration.Id)).ContinueWith(
                                                                    async (tenantIdentityConfigurationEntityTask) =>
                                                                    {
                                                                        var tenantIdentityConfiguration
                                                                            = tenantIdentityConfigurationEntityTask.Result;

                                                                        await CreateTenantConfigurationsAsync(
                                                                            tenantIdentityConfiguration.Id);

                                                                        await _unitOfWork.CommitAsync();

                                                                    }).Unwrap();

                                               }).Unwrap();

                                   await _transaction.CommitAsync();

                                   return new OkObjectResult(
                                       new ApiResponse<TenantResponse>(
                                           true,
                                           HttpStatusCode.Created,
                                           tenant.ToResponse(), [new DadosNotificacao("Tenant criado com sucesso!")]));

                               }).Unwrap();
                    }
                    catch (Exception exception)
                    {
                        Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n");

                        await _transaction.RollbackAsync(); throw;
                    }

                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    /// Recupera todos os dados de tenants.
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<ObjectResult> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(TenantService)} - METHOD {nameof(GetAllAsync)}\n");

        try
        {
            return await _tenantRepository.GetAllAsyncPaginated(pageNumber, pageSize, null)
                    .ContinueWith(taskResult =>
                    {
                        var pagination
                                = taskResult.Result;

                        return new OkObjectResult(
                            new PaginationApiResponse<TenantResponse>(
                                true,
                                HttpStatusCode.OK,
                                pagination.ConvertPaginationData
                                    (pagination.Items.Select(
                                        tenant => tenant.ToResponse()).ToList()), [
                                            new DadosNotificacao("Tenants reuperados com sucesso!")]));
                    });
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    ///  Método responsável por criar um usuário no tenant.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="tenantKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundTenantException"></exception>
    /// <exception cref="NotPermissionTenantException"></exception>
    /// <exception cref="CreateUserFailedException"></exception>
    public async Task<ObjectResult> RegisterTenantUserAsync(
        RegisterUserRequest registerUserRequest, string tenantKey, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(TenantService)} - METHOD {nameof(RegisterTenantUserAsync)}\n");

        try
        {
            await new RegisterUserRequestValidator()
                .ValidateAsync(registerUserRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false)
                            await validation.GetValidationErrors();

                    }).Unwrap();

            return await _tenantRepository.GetAsync(tenant => tenant.TenantConfiguration.TenantKey.Equals(tenantKey))
                .ContinueWith(async (taskResut) =>
                {
                    var tenantEntity
                      = taskResut.Result
                         ?? throw new NotFoundTenantException(tenantKey);

                    if (!tenantEntity.UserId.Equals(_contextService.GetCurrentUserId()))
                        throw new NotPermissionTenantException();

                    var userEntity
                       = registerUserRequest.ToUserTenantEntity(tenantEntity.Id);

                    return await _customUserManager.CreateAsync(
                         userEntity, registerUserRequest.Password)
                            .ContinueWith(identityResultTask =>
                            {
                                var identityResult
                                        = identityResultTask.Result;

                                if (identityResult.Succeeded is false)
                                    throw new CreateUserFailedException(
                                        registerUserRequest, identityResult.Errors.Select((e)
                                            => new DadosNotificacao(e.Description)).ToList());

                                return new OkObjectResult(
                                    new ApiResponse<UserResponse>(
                                        identityResult.Succeeded,
                                            HttpStatusCode.Created,
                                            userEntity.ToResponse(), [
                                                 new DadosNotificacao("Usuário criado com sucesso e vinculado ao Tenant!")]));
                            });

                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    /// Cria todas as configurações do tenant.
    /// </summary>
    /// <param name="tenantIdentityConfigurationId"></param>
    /// <returns></returns>
    private async Task CreateTenantConfigurationsAsync(
        Guid tenantIdentityConfigurationId) =>
            await Task.WhenAll(

                _userIdentityConfigurationRepository.CreateAsync(
                    CreateUserIdentityConfiguration.CreateDefault(
                        tenantIdentityConfigurationId)
                    ),

                _passwordIdentityConfigurationRepository.CreateAsync(
                    CreatePasswordIdentityConfiguration.CreateDefault(
                            tenantIdentityConfigurationId)
                    ),

                _lockoutIdentityConfigurationRepository.CreateAsync(
                    CreateLockoutIdentityConfiguration.CreateDefault(
                        tenantIdentityConfigurationId)
                    )
                );
}
