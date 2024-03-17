using AUTHIO.APPLICATION.Application.Configurations.Extensions;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories;
using AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Services.System;
using AUTHIO.APPLICATION.Domain.Dtos.Response;
using AUTHIO.APPLICATION.Domain.Dtos.Response.Base;
using AUTHIO.APPLICATION.Domain.Exceptions;
using AUTHIO.APPLICATION.Domain.Utils.Extensions;
using AUTHIO.APPLICATION.Domain.Validators;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using AUTHIO.APPLICATION.DOMAIN.DTOs.CONFIGURATIONS.AUTH.TOKEN;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST;
using AUTHIO.APPLICATION.DOMAIN.DTOs.REQUEST.SYSTEM;
using AUTHIO.APPLICATION.DOMAIN.VALIDATORS;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Data;
using System.Net;
using static AUTHIO.APPLICATION.Domain.Exceptions.CustomUserException;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AUTHIO.APPLICATION.Application.Services.System;

/// <summary>
/// Serviço de Authenticação do sistema.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public sealed class AuthenticationService(
    ITokenService tokenService,
    IUserRepository userRepository,
    IUnitOfWork<AuthIoContext> unitOfWork) : IAuthenticationService
{
    private readonly ITokenService _tokenService = tokenService;

    private readonly IUnitOfWork<AuthIoContext>
        _unitOfWork = unitOfWork;

    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Método de registro de usuário.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="CreateUserFailedException"></exception>
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

            var transaction =
                await _unitOfWork.BeginTransactAsync();

            try
            {
                var userEntity
                    = registerUserRequest.ToUserSystemEntity();

                return await _userRepository
                    .CreateUserAsync(userEntity, registerUserRequest.Password).ContinueWith(async (identityResultTask) =>
                    {
                        var identityResult
                                = identityResultTask.Result;

                        if (identityResult.Succeeded is false)
                            throw new CreateUserFailedException(
                                registerUserRequest, identityResult.Errors.Select((e)
                                    => new DadosNotificacao(e.Code.CustomExceptionMessage())).ToList());

                        return await _userRepository
                            .AddToUserRoleAsync(userEntity, "System").ContinueWith(identityResultTask =>
                            {
                                var identityResult
                                        = identityResultTask.Result;

                                if (identityResult.Succeeded is false)
                                    throw new UserToRoleFailedException(
                                        registerUserRequest, identityResult.Errors.Select((e)
                                            => new DadosNotificacao(e.Code.CustomExceptionMessage())).ToList());

                                transaction.CommitAsync();

                                return new OkObjectResult(
                                    new ApiResponse<UserResponse>(
                                        identityResult.Succeeded,
                                        HttpStatusCode.Created,
                                        userEntity.ToResponse(), [
                                            new DadosNotificacao("Usuário criado com sucesso!")]));
                            });

                    }).Unwrap();
            }
            catch 
            {
                transaction.Rollback(); throw;
            }
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception: {exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    /// Método responsável por fazer a authorização do usuário.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundUserException"></exception>
    public async Task<ObjectResult> AuthenticationAsync(LoginRequest loginRequest)
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(AuthenticationService)} - METHOD {nameof(AuthenticationAsync)}\n");

        try
        {
            await new LoginRequestValidator().ValidateAsync(
                loginRequest).ContinueWith(async (validationTask) =>
                {
                    var validation = validationTask.Result;

                    if (validation.IsValid is false) await validation.GetValidationErrors();

                }).Unwrap();

            return await _userRepository.GetWithUsernameAsync(
                loginRequest.Username).ContinueWith(async (userEntityTask) =>
                {
                    var userEntity =
                        userEntityTask.Result
                        ?? throw new NotFoundUserException(loginRequest);

                    await _userRepository.PasswordSignInAsync(
                        userEntity, loginRequest.Password, true, true).ContinueWith((signInResultTask) =>
                        {
                            var signInResult = signInResultTask.Result;

                            if (signInResult.Succeeded is false)
                                ThrownAuthorizationException(signInResult, userEntity.Id, loginRequest);
                        });

                    Log.Information(
                        $"[LOG INFORMATION] - Usuário autenticado com sucesso!\n");

                    return await GenerateTokenJwtAsync(loginRequest).ContinueWith(
                        (tokenJwtTask) =>
                        {
                            var tokenJWT =
                                tokenJwtTask.Result
                                ?? throw new TokenJwtException(null);

                            Log.Information(
                                $"[LOG INFORMATION] - Token gerado com sucesso {JsonConvert.SerializeObject(tokenJWT)}!\n");

                            return new OkObjectResult(
                                new ApiResponse<TokenJWT>(
                                    true, HttpStatusCode.Created, tokenJWT, [
                                        new("Token criado com sucesso!")]));
                        });

                }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception:{exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); throw;
        }
    }

    /// <summary>
    /// Método responsavel por gerar um tokenJwt.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    private async Task<TokenJWT> GenerateTokenJwtAsync(LoginRequest loginRequest) 
        => await _tokenService.CreateJsonWebToken(loginRequest.Username).ContinueWith((tokenTask) => 
        {
            var tokenJwt =
                tokenTask.Result
                ?? throw new TokenJwtException(loginRequest);

            return tokenJwt;
        });

    /// <summary>
    /// Método responsável por tratar os erros de autenticação.
    /// </summary>
    /// <param name="signInResult"></param>
    /// <returns></returns>
    /// <exception cref="LockedOutAuthenticationException"></exception>
    /// <exception cref="IsNotAllowedAuthenticationException"></exception>
    /// <exception cref="RequiresTwoFactorAuthenticationException"></exception>
    /// <exception cref="InvalidUserAuthenticationException"></exception>
    private static void ThrownAuthorizationException(SignInResult signInResult, Guid userId, LoginRequest loginRequest)
    {
        if (signInResult.IsLockedOut)
        {
            Log.Information($"[LOG INFORMATION] - Falha ao autenticar usuário, está bloqueado.\n");

            throw new LockedOutAuthenticationException(loginRequest);
        }
        else if (signInResult.IsNotAllowed)
        {
            Log.Information($"[LOG INFORMATION] - Falha ao autenticar usuário, não está confirmado.\n");

            throw new IsNotAllowedAuthenticationException(new
            {
                userId,
                isNotAllowed = true,
                loginRequest
            });
        }
        else if (signInResult.RequiresTwoFactor)
        {
            Log.Information($"[LOG INFORMATION] - Falha ao autenticar usuário, requer verificação de dois fatores.\n");

            throw new RequiresTwoFactorAuthenticationException(loginRequest);
        }
        else
        {
            Log.Information($"[LOG INFORMATION] - Falha na autenticação dados incorretos!\n");

            throw new InvalidUserAuthenticationException(loginRequest);
        }
    }

}
