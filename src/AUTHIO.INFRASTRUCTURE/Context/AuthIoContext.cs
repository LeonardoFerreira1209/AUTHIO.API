﻿using AUTHIO.DOMAIN.Contracts.Repositories.Store;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Model;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AUTHIO.INFRASTRUCTURE.Context;

/// <summary>
/// Classe de contexto.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="options"></param>
public sealed class AuthIoContext(
    DbContextOptions<AuthIoContext> options, IContextService contextService)
        : IdentityDbContext<UserEntity, RoleEntity, Guid>(options), IAuthioContext, ISecurityKeyContext
{
    public Guid? CurrentUserId { get; init; }
       = contextService.IsAuthenticated ? contextService.GetCurrentUserId() : null;

    public Guid? TenantId { get; init; }
        = contextService?.GetCurrentTenantId();

    public string TenantKey { get; init; }
        = contextService?.GetCurrentTenantKey();

    /// <summary>
    /// Tabela de Tenants.
    /// </summary>
    public DbSet<TenantEntity> Tenants => Set<TenantEntity>();

    /// <summary>
    /// Tabela de Tenant Configurations.
    /// </summary>
    public DbSet<TenantConfigurationEntity> TenantConfigurations => Set<TenantConfigurationEntity>();

    /// <summary>
    /// Tabela de Tenant Identity Configurations.
    /// </summary>
    public DbSet<TenantIdentityConfigurationEntity> TenantIdentityConfigurations => Set<TenantIdentityConfigurationEntity>();

    /// <summary>
    /// Tabela de User Identity Configurations.
    /// </summary>
    public DbSet<UserIdentityConfigurationEntity> UserIdentityConfigurations => Set<UserIdentityConfigurationEntity>();

    /// <summary>
    /// Tabela de Password Identity Configurations.
    /// </summary>
    public DbSet<PasswordIdentityConfigurationEntity> PasswordIdentityConfigurations => Set<PasswordIdentityConfigurationEntity>();

    /// <summary>
    /// Tabela de Lockout Identity Configurations.
    /// </summary>
    public DbSet<LockoutIdentityConfigurationEntity> LockoutIdentityConfigurations => Set<LockoutIdentityConfigurationEntity>();

    /// <summary>
    /// Tabela de Tenant Email Configurations.
    /// </summary>
    public DbSet<TenantEmailConfigurationEntity> TenantEmailConfigurations => Set<TenantEmailConfigurationEntity>();

    /// <summary>
    /// Tabela de Tenant Token Configurations.
    /// </summary>
    public DbSet<TenantTokenConfigurationEntity> TenantTokenConfigurations => Set<TenantTokenConfigurationEntity>();

    /// <summary>
    /// Tabela de configuração do SendGrid.
    /// </summary>
    public DbSet<SendGridConfigurationEntity> SendGridConfigurations => Set<SendGridConfigurationEntity>();

    /// <summary>
    /// Tabela de Feature Flags.
    /// </summary>
    public DbSet<FeatureFlagsEntity> FeatureFlags => Set<FeatureFlagsEntity>();

    /// <summary>
    /// Tabela de Events.
    /// </summary>
    public DbSet<EventEntity> Events => Set<EventEntity>();

    /// <summary>
    /// Tabela de chaves de segurança.
    /// </summary>
    public DbSet<KeyMaterial> SecurityKeys { get; set; }

    /// <summary>
    /// Tabela de assinaturas.
    /// </summary>
    public DbSet<SubscriptionEntity> Subscriptions { get; set; }

    /// <summary>
    /// Tabela de planos.
    /// </summary>
    public DbSet<PlanEntity> Plans { get; set; }

    /// <summary>
    /// On model creating.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
           .ApplyConfiguration(new UserEntityTypeConfiguration())
           .ApplyConfiguration(new UserLoginEntityTypeConfiguration()) 
           .ApplyConfiguration(new UserTokenEntityTypeConfiguration())
           .ApplyConfiguration(new RoleEntityTypeConfiguration())
           .ApplyConfiguration(new UserRoleEntityTypeConfiguration())
           .ApplyConfiguration(new RoleClaimEntityTypeConfiguration())
           .ApplyConfiguration(new UserClaimEntityTypeConfiguration())
           .ApplyConfiguration(new UserIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new TenantEntityTypeConfiguration())
           .ApplyConfiguration(new TenantIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new TenantUserAdminEntityTypeConfiguration())
           .ApplyConfiguration(new PasswordIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new LockoutIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new KeyMaterialEntityTypeConfiguration());

        var roleEntity = new RoleEntity
        {
            Id = Guid.NewGuid(),
            Name = "System",
            NormalizedName = "SYSTEM",
            System = true,
            Created = DateTime.Now,
            Status = Status.Ativo,
        };

        modelBuilder.Entity<RoleEntity>().HasData(
        [
            roleEntity
        ]);

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData([

            new IdentityRoleClaim<Guid>{
                Id = 1,
                RoleId = roleEntity.Id,
                ClaimType = "Tenants",
                ClaimValue = "POST"
            },
            new IdentityRoleClaim<Guid>{
                Id = 2,
                RoleId = roleEntity.Id,
                ClaimType = "Tenants",
                ClaimValue = "GET"
            },
            new IdentityRoleClaim<Guid>{
                Id = 3,
                RoleId = roleEntity.Id,
                ClaimType = "Tenants",
                ClaimValue = "PATCH"
            },
            new IdentityRoleClaim<Guid>{
                Id = 4,
                RoleId = roleEntity.Id,
                ClaimType = "Tenants",
                ClaimValue = "PUT"
            }
        ]);
    }
}
