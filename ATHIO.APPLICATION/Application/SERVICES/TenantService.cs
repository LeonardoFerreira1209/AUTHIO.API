using AUTHIO.APPLICATION.Application.Configurations.Extensions;
using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services;
using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.Domain.Entity;
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
    ITenantRepository tenantRepository, IContextService contextService)
        : ITenantService
{
    private readonly IUnitOfWork<AuthIoContext>
       _unitOfWork = unitOfWork;

    private readonly ITenantRepository
        _tenantRepository = tenantRepository;

    private readonly IContextService
        _contextService = contextService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="createTenantRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="CreateUserFailedException"></exception>
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

                        if (validation.IsValid is false) await validation.GetValidationErrors();

                    }).Unwrap();

            return await _tenantRepository.GetAsync(tenant => tenant.Name == createTenantRequest.Name)
                .ContinueWith(async (tenantResult) =>
                {
                    if(tenantResult.Result is not null) 
                        throw new DuplicatedTenantException(createTenantRequest);

                    return await _tenantRepository.CreateAsync(
                        createTenantRequest.ToEntity(_contextService.GetCurrentUserId()))
                            .ContinueWith(async (tenantEntityTask) =>
                            {
                                var tenant
                                    = tenantEntityTask.Result;

                                await _unitOfWork.CommitAsync();

                                return new OkObjectResult(
                                    new ApiResponse<TenantEntity>(
                                        true,
                                        HttpStatusCode.Created,
                                        tenant, [new DadosNotificacao("Tenant criado com sucesso!")]));

                            }).Unwrap();

                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }
}
