using AUTHIO.APPLICATION.Domain.Entities;
using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.DOMAIN.CONTRACTS.SERVICES.SYSTEM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AUTHIO.APPLICATION.Infra.Context;

/// <summary>
/// Classe de contexto.
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="options"></param>
public class AuthIoContext(
    DbContextOptions<AuthIoContext> options, IContextService contextService)
        : IdentityDbContext<UserEntity, RoleEntity, Guid>(options)
{
    public readonly Guid? _currentUserId
        = contextService.IsAuthenticated ? contextService.GetCurrentUserId() : null;

    public readonly Guid? _tenantId
        = contextService.GetCurrentTenantId();

    public readonly string _apiKey
        = contextService.GetCurrentApiKey();

    /// <summary>
    /// Tabela de Tenants.
    /// </summary>
    public DbSet<TenantEntity> Tenants => Set<TenantEntity>();

    /// <summary>
    /// Tabela de Tenant Configurations.
    /// </summary>
    public DbSet<TenantConfigurationEntity> TenantConfigurations => Set<TenantConfigurationEntity>();

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

        modelBuilder.Entity<RoleEntity>()
            .HasOne(x => x.Tenant)
            .WithMany(t => t.Roles)
            .HasForeignKey(x => x.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Tenant)
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.TenantId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TenantEntity>()
            .HasOne(t => t.TenantConfiguration)
            .WithOne(tc => tc.Tenant)
            .HasForeignKey<TenantConfigurationEntity>(tc => tc.TenantId);

        modelBuilder.Entity<TenantUserAdminEntity>()
            .HasKey(tua => new { tua.UserId, tua.TenantId });

        modelBuilder.Entity<TenantUserAdminEntity>()
           .HasOne(tua => tua.User)
           .WithMany()
           .HasForeignKey(tua => tua.UserId);

        modelBuilder.Entity<TenantUserAdminEntity>()
            .HasOne(tua => tua.Tenant)
            .WithMany(t => t.UserAdmins)
            .HasForeignKey(tua => tua.TenantId);

        var roleEntity = new RoleEntity
        {
            Id = Guid.NewGuid(),
            Name = "System",
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
