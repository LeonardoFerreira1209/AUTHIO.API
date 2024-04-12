using AUTHIO.APPLICATION.Application.Configurations.Extensions;
using AUTHIO.APPLICATION.Domain.Builders.Creates;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services;
using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.Domain.Exceptions;
using AUTHIO.APPLICATION.Domain.Utils.Extensions;
using AUTHIO.APPLICATION.Domain.Validators;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomTenantExceptions;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.APPLICATION.SERVICES;

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
    IContextService contextService,
    IUserRepository userRepository) : ITenantService
{
    private readonly IUnitOfWork<AuthIoContext>
       _unitOfWork = unitOfWork;

    private readonly ITenantRepository
        _tenantRepository = tenantRepository;

    private readonly ITenantConfigurationRepository
        _tenantConfigurationRepository = tenantConfigurationRepository;

    private readonly ITenantIdentityConfigurationRepository
        _tenantIdentityConfigurationRepository = tenantIdentityConfigurationRepository;

    private readonly IContextService
        _contextService = contextService;

    private readonly IUserRepository
        _userRepository = userRepository;

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
                                        CreateTenantConfiguration.CreateDefaultTenantConfiguration(
                                            tenant.Id)).ContinueWith(
                                               async (tenantConfigurationEntityTask) =>
                                               {
                                                   var tenantConfiguration
                                                        = tenantConfigurationEntityTask.Result;

                                                   await _tenantIdentityConfigurationRepository.CreateAsync(
                                                       CreateTenantIdentityConfiguration.CreateDefaultTenantIdnetityConfiguration(
                                                                tenantConfiguration.Id)).ContinueWith(
                                                                    async (tenantIdentityConfigurationEntityTask) =>
                                                                    {
                                                                        var tenantConfiguration
                                                                            = tenantIdentityConfigurationEntityTask.Result;

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
            return await _tenantRepository.GetAllAsyncPaginated(pageNumber, pageSize)
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
            Log.Error($"[LOG ERROR] - Exception: {
                exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    ///  Método responsável por criar um usuário no tenant.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="apiKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundTenantException"></exception>
    /// <exception cref="NotPermissionTenantException"></exception>
    /// <exception cref="CreateUserFailedException"></exception>
    public async Task<ObjectResult> RegisterTenantUserAsync(
        RegisterUserRequest registerUserRequest, string apiKey, CancellationToken cancellationToken)
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

            return await _tenantRepository.GetAsync(tenant => tenant.TenantConfiguration.ApiKey.Equals(apiKey))
                .ContinueWith(async (taskResut) =>
                {
                    var tenantEntity
                      = taskResut.Result
                         ?? throw new NotFoundTenantException(apiKey);

                    if (!tenantEntity.UserId.Equals(_contextService.GetCurrentUserId()))
                        throw new NotPermissionTenantException();

                    var userEntity
                       = registerUserRequest.ToUserTenantEntity(tenantEntity.Id);

                    return await _userRepository.CreateUserAsync(
                         userEntity, registerUserRequest.Password)
                            .ContinueWith(identityResultTask =>
                            {
                                var identityResult
                                        = identityResultTask.Result;

                                if (identityResult.Succeeded is false)
                                    throw new CreateUserFailedException(
                                        registerUserRequest, identityResult.Errors.Select((e)
                                            => new DadosNotificacao(e.Code.CustomExceptionMessage())).ToList());

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
}
