using AUTHIO.APPLICATION.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AUTHIO.APPLICATION.Infra.Context;

/// <summary>
/// Classe de contexto.
/// </summary>
public class AuthIoContext
    : IdentityDbContext<UserEntity, RoleEntity, Guid>
{

    private readonly Guid _tenantId;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="options"></param>
    public AuthIoContext(
        DbContextOptions<AuthIoContext> options) : base(options)
    {
        _tenantId = Guid.Parse("8A7CD46D-87B5-498A-15F4-08DC236A8A23");
        Database.EnsureCreated();
    }

    /// <summary>
    /// Tabela de Tenants.
    /// </summary>
    public DbSet<TenantEntity> Tenants => Set<TenantEntity>();

    /// <summary>
    /// On model creating.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
            var type = entity.ClrType;
            if (typeof(IEntityTenant).IsAssignableFrom(type)) {
                var method = typeof(AuthIoContext)
                    .GetMethod(nameof(TenantFilterExpression), 
                    BindingFlags.NonPublic | BindingFlags.Static
                        )?.MakeGenericMethod(type);

                var expression = method?.Invoke(null, new object[] { this })!;
                entity.SetQueryFilter((LambdaExpression)expression);
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

        modelBuilder.Entity<TenantUserAdminEntity>()
            .HasKey(ta => new { ta.UserId, ta.TenantId });

        modelBuilder.Entity<TenantUserAdminEntity>()
            .HasOne(ta => ta.Tenant)
            .WithMany(t => t.UserAdmins)
            .HasForeignKey(ta => ta.TenantId);
    }

    /// <summary>
    /// Prepara a expressão de filtragem global por tenantId.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="authIoContext"></param>
    /// <returns></returns>
    private static Expression<Func<TEntity, bool>> TenantFilterExpression<TEntity>(
            AuthIoContext authIoContext)
            where TEntity : class, IEntityTenant 
    {
        Expression<Func<TEntity, bool>> expression 
            = x => x.TenantId == authIoContext._tenantId;

        return expression;
    }
}
