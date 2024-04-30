using AUTHIO.DATABASE.Context.EntityTypeConfigurations;
using AUTHIO.DOMAIN.Contracts;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AUTHIO.DATABASE.Context;

/// <summary>
/// Classe de contexto.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="options"></param>
public sealed class AuthIoContext(
    DbContextOptions<AuthIoContext> options, IContextService contextService)
        : IdentityDbContext<UserEntity, RoleEntity, Guid>(options), IAuthioContext
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
    /// Tabela de Feature Flags.
    /// </summary>
    public DbSet<FeatureFlagsEntity> FeatureFlags => Set<FeatureFlagsEntity>();

   
    /// <summary>
    /// On model creating.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var entityClrType = entityType.ClrType;
            var filterableInterfaceType = typeof(IFilterableEntity<>).MakeGenericType(entityClrType);
            if (filterableInterfaceType.IsAssignableFrom(entityClrType))
            {
                var instance = Activator.CreateInstance(entityClrType);
                var methodInfo = filterableInterfaceType.GetMethod("GetFilterExpression");
                var filterExpression = methodInfo.Invoke(instance, [this]);

                modelBuilder.Entity(entityClrType).HasQueryFilter((LambdaExpression)filterExpression);
            }
        }

        modelBuilder
           .ApplyConfiguration(new UserEntityTypeConfiguration())
           .ApplyConfiguration(new RoleEntityTypeConfiguration())
           .ApplyConfiguration(new TenantEntityTypeConfiguration())
           .ApplyConfiguration(new TenantIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new UserIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new PasswordIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new TenantUserAdminEntityTypeConfiguration());

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

        modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(
            [
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
