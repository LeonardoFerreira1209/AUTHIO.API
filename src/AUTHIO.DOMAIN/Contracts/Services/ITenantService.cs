﻿using AUTHIO.DOMAIN.Dtos.Request;
using Microsoft.AspNetCore.Mvc;

namespace AUTHIO.DOMAIN.Contracts.Services;

/// <summary>
/// Interface de Tenants.
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Método responsável por criar um tenant.
    /// </summary>
    /// <param name="createTenantRequest"></param>
    /// <returns></returns>
    Task<ObjectResult> CreateAsync(
        CreateTenantRequest createTenantRequest, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<ObjectResult> GetAllAsync(int pageNumber, int PageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Método responsável por criar um usuário no tenant.
    /// </summary>
    /// <param name="registerUserRequest"></param>
    /// <param name="tenantKey"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ObjectResult> RegisterTenantUserAsync(
        RegisterUserRequest registerUserRequest, string tenantKey, CancellationToken cancellationToken);
}