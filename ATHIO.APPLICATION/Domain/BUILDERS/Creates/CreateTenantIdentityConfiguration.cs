﻿using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;

namespace AUTHIO.APPLICATION.Domain.Builders.Creates;

/// <summary>
/// Classe de criação de Configuração de identity do tenant.
/// </summary>
public static class CreateTenantIdentityConfiguration
{
    /// <summary>
    /// Cria um tenant identity configuration com os dados de cadastro inicial.
    /// </summary>
    /// <param name="apikey"></param>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public static TenantIdentityConfigurationEntity CreateDefaultTenantIdnetityConfiguration(Guid tenantConfigurationId) 
        => new TenantIdentityConfigurationBuilder()
                .AddTenantConfigurationId(tenantConfigurationId)
                    .AddCreated()
                        .AddStatus(Status.Ativo).Builder();
}