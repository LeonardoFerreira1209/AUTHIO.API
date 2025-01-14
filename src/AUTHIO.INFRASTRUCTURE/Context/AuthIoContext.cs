using AUTHIO.DOMAIN.Contracts.Repositories.Store;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Model;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.INFRASTRUCTURE.Context.EntityTypeConfigurations;
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
        : IdentityDbContext<
            UserEntity, 
            RoleEntity, 
            Guid, 
            UserClaimEntity, 
            UserRoleEntity, 
            UserLoginEntity, 
            RoleClaimEntity, 
            UserTokenEntity
        >(options), IAuthioContext, ISecurityKeyContext
{
    public Guid? CurrentUserId { get; init; }
       = contextService.IsAuthenticated ? contextService.GetCurrentUserId() : null;

    public Guid? ClientId { get; init; }
        = contextService?.GetCurrentClientId();

    public string ClientKey { get; init; }
        = contextService?.GetCurrentClientKey();

    /// <summary>
    /// Tabela de Realms.
    /// </summary>
    public DbSet<RealmEntity> Realms => Set<RealmEntity>();

    /// <summary>
    /// Tabela de Clients.
    /// </summary>
    public DbSet<ClientEntity> Clients => Set<ClientEntity>();

    /// <summary>
    /// Tabela de Client Configurations.
    /// </summary>
    public DbSet<ClientConfigurationEntity> ClientConfigurations => Set<ClientConfigurationEntity>();

    /// <summary>
    /// Tabela de Client Identity Configurations.
    /// </summary>
    public DbSet<ClientIdentityConfigurationEntity> ClientIdentityConfigurations => Set<ClientIdentityConfigurationEntity>();

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
    /// Tabela de Client Email Configurations.
    /// </summary>
    public DbSet<ClientEmailConfigurationEntity> ClientEmailConfigurations => Set<ClientEmailConfigurationEntity>();

    /// <summary>
    /// Tabela de Client Token Configurations.
    /// </summary>
    public DbSet<ClientTokenConfigurationEntity> ClientTokenConfigurations => Set<ClientTokenConfigurationEntity>();

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
           .ApplyConfiguration(new RealmIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new ClientEntityTypeConfiguration())
           .ApplyConfiguration(new ClientIdentityConfigurationEntityTypeConfiguration())
           .ApplyConfiguration(new ClientUserAdminEntityTypeConfiguration())
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

        modelBuilder.Entity<RoleClaimEntity>().HasData([

            new RoleClaimEntity{
                Id = 1,
                RoleId = roleEntity.Id,
                ClaimType = "Clients",
                ClaimValue = "POST"
            },
            new RoleClaimEntity{
                Id = 2,
                RoleId = roleEntity.Id,
                ClaimType = "Clients",
                ClaimValue = "GET"
            },
            new RoleClaimEntity{
                Id = 3,
                RoleId = roleEntity.Id,
                ClaimType = "Clients",
                ClaimValue = "PATCH"
            },
            new RoleClaimEntity{
                Id = 4,
                RoleId = roleEntity.Id,
                ClaimType = "Clients",
                ClaimValue = "PUT"
            }
        ]);
    }
}
