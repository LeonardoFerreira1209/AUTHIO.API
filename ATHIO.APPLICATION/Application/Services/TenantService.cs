using AUTHIO.APPLICATION.Application.Configurations.Extensions;
using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services;
using AUTHIO.APPLICATION.Domain.Dtos.Request;
using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Domain.Exceptions;
using AUTHIO.APPLICATION.Domain.Utils.Extensions;
using AUTHIO.APPLICATION.Domain.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomTenantExceptions;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.Application.Services;

/// <summary>
/// Serviço de tenants.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public sealed class TenantService(
    IUnitOfWork unitOfWork,
    ITenantRepository tenantRepository,
    UserManager<UserEntity> userManager) : ITenantService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;

    /// <summary>
    /// Método responsável por criar e configurar um tenant.
    /// </summary>
    /// <param name="tenantProvisionRequest"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ObjectResult> TenantProvisionAsync(TenantProvisionRequest tenantProvisionRequest)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(TenantService)} - METHOD {nameof(TenantProvisionAsync)}\n");

        await new TenantProvisionRequestValidator().ValidateAsync(
            tenantProvisionRequest).ContinueWith(async (validationTask) =>
            {
                var validation = validationTask.Result;

                if (validation.IsValid is false) await validation.GetValidationErrors();

            }).Unwrap();

        return await _unitOfWork.BeginTransactAsync().ContinueWith(
            async (transactionResult) => {
                var transaction = transactionResult.Result;

                try
                {
                    var (tenantEntity, userEntity) =
                        tenantProvisionRequest.ToEntities();

                    return await _tenantRepository.CreateAsync(
                        tenantEntity).ContinueWith(async (tenantEntityTask) =>
                        {
                            var tenantEntity =
                                tenantEntityTask.Result
                                ?? throw new CreateTenantFailedException(tenantProvisionRequest);

                            userEntity.TenantId = tenantEntity.Id;

                            await _userManager.CreateAsync(
                                userEntity, tenantProvisionRequest.UserAdmin.Password).ContinueWith(async (identityResultTask) =>
                                {
                                    var identityResult
                                        = identityResultTask.Result;

                                    if (identityResult.Succeeded is false)
                                        throw new CreateUserFailedException(
                                            tenantProvisionRequest.UserAdmin, identityResult.Errors.Select((e)
                                                => new DadosNotificacao(e.Code.CustomExceptionMessage())).ToList());

                                    await _tenantRepository.LinkTenantWithUserAdminAsync(
                                        tenantEntity.Id, userEntity.Id);

                                    await _unitOfWork.CommitAsync();

                                }).Unwrap();

                            await transaction.CommitAsync();

                            return new OkObjectResult(
                                new ApiResponse<object>(
                                    true,
                                    HttpStatusCode.Created,
                                    tenantEntity.ToResponse(),
                                    [new DadosNotificacao("Tenant criado com sucesso!")]));

                        }).Unwrap();
                }
                catch (Exception exception)
                {
                    await transaction.RollbackAsync();

                    Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
                }

            }).Unwrap();
    }
}
