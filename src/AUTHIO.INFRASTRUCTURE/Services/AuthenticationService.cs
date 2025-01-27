﻿using AUTHIO.DOMAIN.Auth.Token;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Helpers.Expressions.Filters;
using AUTHIO.DOMAIN.Helpers.Extensions;
using AUTHIO.DOMAIN.Validators;
using AUTHIO.INFRASTRUCTURE.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using static AUTHIO.DOMAIN.Exceptions.CustomUserException;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviço de Authenticação do sistema.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
public sealed class AuthenticationService(
    ITokenService tokenService,
    CustomUserManager<UserEntity> customUserManager,
    CustomSignInManager<UserEntity> customSignInManager) : IAuthenticationService
{
    /// <summary>
    /// Método responsável por fazer a authorização do usuário.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <param name="currentTanantKey"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundUserException"></exception>
    /// <exception cref="TokenJwtException"></exception>
    public async Task<ObjectResult> AuthenticationAsync(
        LoginRequest loginRequest, 
        string currentTanantKey
        )
    {
        Log.Information(
            $"[LOG INFORMATION] - SET TITLE {nameof(AuthenticationService)} - METHOD {nameof(AuthenticationAsync)}\n");

        try
        {
            await new LoginRequestValidator().ValidateAsync(
                loginRequest).ContinueWith(async (validationTask) =>
                {
                    var validation = validationTask.Result;

                    if (validation.IsValid is false)
                        await validation.GetValidationErrors();

                }).Unwrap();

            return await customUserManager.FindByNameWithExpressionAsync(
                loginRequest.Username,
                UserFilters<UserEntity>.FilterSystemOrTenantUsers(
                    currentTanantKey
                )

            ).ContinueWith(async (userEntityTask) =>
            {
                var userEntity =
                    userEntityTask.Result
                    ?? throw new NotFoundUserException(loginRequest);

                await customSignInManager.PasswordSignInAsync(
                    userEntity, loginRequest.Password, true, true).ContinueWith((signInResultTask) =>
                    {
                        var signInResult = signInResultTask.Result;

                        if (signInResult.Succeeded is false)
                            ThrownAuthorizationException(signInResult, userEntity.Id, loginRequest);
                    });

                Log.Information(
                    $"[LOG INFORMATION] - Usuário autenticado com sucesso!\n");

                return await GenerateTokenJwtAsync(loginRequest).ContinueWith(
                    (tokenJwtTask) => {

                        var tokenJWT =
                            tokenJwtTask.Result
                            ?? throw new TokenJwtException(null);

                        Log.Information(
                            $"[LOG INFORMATION] - Token gerado com sucesso {JsonConvert.SerializeObject(tokenJWT)}!\n");

                        ObjectResponse response = new(
                            HttpStatusCode.Created,
                            new ApiResponse<TokenJWT>(
                                true, HttpStatusCode.Created, tokenJWT, [
                                    new("Token criado com sucesso!")
                                ]
                            )
                        );

                        return response;
                    }
                );

            }).Unwrap();
        }
        catch (Exception exception)
        {
            Log.Error($"[LOG ERROR] - Exception:{exception.Message} - {JsonConvert.SerializeObject(exception)}\n"); 
            
            throw;
        }
    }

    /// <summary>
    /// Método responsavel por gerar um tokenJwt.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    private async Task<TokenJWT> GenerateTokenJwtAsync(LoginRequest loginRequest)
        => await tokenService.CreateJsonWebToken(
            loginRequest.Username).ContinueWith((tokenTask) =>
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
    private static void ThrownAuthorizationException(
        SignInResult signInResult, Guid userId, LoginRequest loginRequest)
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
