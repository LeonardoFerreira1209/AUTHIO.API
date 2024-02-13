using AUTHIO.APPLICATION.Application.Configurations.Extensions;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services.System;
using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.Domain.Exceptions;
using AUTHIO.APPLICATION.Domain.Utils.Extensions;
using AUTHIO.APPLICATION.Domain.Validators;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.REPOSITORY;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST.SYSTEM;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Data;
using System.Net;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomUserException;

namespace AUTHIO.APPLICATION.Application.Services.System;

/// <summary>
/// Serviço de Authenticação do sistema.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public sealed class AuthenticationService(
    IUserRepository userRepository,
    IUnitOfWork<AuthIoContext> unitOfWork
    ) : IAuthenticationService
{
    private readonly IUnitOfWork<AuthIoContext> 
        _unitOfWork = unitOfWork;

    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Método de registro de usuário.
    /// </summary>
    /// <param name="registerSystemUserRequest"></param>
    /// <returns></returns>
    public async Task<ObjectResult> RegisterAsync(RegisterUserRequest registerUserRequest, CancellationToken cancellationToken)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(AuthenticationService)} - METHOD {nameof(RegisterAsync)}\n");

        try
        {
            await new RegisterUserRequestValidator()
                .ValidateAsync(registerUserRequest, cancellationToken)
                    .ContinueWith(async (validationTask) =>
                    {
                        var validation = validationTask.Result;

                        if (validation.IsValid is false) await validation.GetValidationErrors();

                    }).Unwrap();

            var userEntity 
                = registerUserRequest.ToEntity();

            return await _userRepository
                    .CreateUserAsync(userEntity, registerUserRequest.Password).ContinueWith(async (identityResultTask) =>
                    {
                        var identityResult
                                = identityResultTask.Result;

                        if (identityResult.Succeeded is false)
                            throw new CreateUserFailedException(
                                registerUserRequest, identityResult.Errors.Select((e)
                                    => new DadosNotificacao(e.Code.CustomExceptionMessage())).ToList());

                        return new OkObjectResult(
                            new ApiResponse<object>(
                                identityResult.Succeeded,
                                HttpStatusCode.Created,
                                userEntity.ToResponse(),
                                [new DadosNotificacao("Usuário criado com sucesso!")]));

                    }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }
}
