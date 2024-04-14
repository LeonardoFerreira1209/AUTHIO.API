﻿// <auto-generated />
using System;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AUTHIO.APPLICATION.Migrations
{
    [DbContext(typeof(AuthIoContext))]
    partial class AuthIoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.FeatureFlagsEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("FeatureFlags");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.PasswordIdentityConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("RequireDigit")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("RequireLowercase")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("RequireNonAlphanumeric")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("RequireUppercase")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RequiredLength")
                        .HasColumnType("int");

                    b.Property<int>("RequiredUniqueChars")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantIdentityConfigurationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TenantIdentityConfigurationId")
                        .IsUnique();

                    b.ToTable("PasswordIdentityConfigurations", (string)null);
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("System")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("8a21a41a-37d9-492a-a5f0-e568ccba1baa"),
                            Created = new DateTime(2024, 4, 14, 2, 1, 10, 851, DateTimeKind.Local).AddTicks(3325),
                            Name = "System",
                            Status = 1,
                            System = true
                        });
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ApiKey")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId")
                        .IsUnique();

                    b.ToTable("TenantConfigurations");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantIdentityConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TenantConfigurationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TenantConfigurationId")
                        .IsUnique();

                    b.ToTable("TenantIdentityConfigurations");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantIdentityUserAdminEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "TenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantUserAdmins", (string)null);
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("System")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.UserIdentityConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AllowedUserNameCharacters")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("RequireUniqueEmail")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("TenantIdentityConfigurationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TenantIdentityConfigurationId")
                        .IsUnique();

                    b.ToTable("UserIdentityConfigurations", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "Tenants",
                            ClaimValue = "POST",
                            RoleId = new Guid("8a21a41a-37d9-492a-a5f0-e568ccba1baa")
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "Tenants",
                            ClaimValue = "GET",
                            RoleId = new Guid("8a21a41a-37d9-492a-a5f0-e568ccba1baa")
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "Tenants",
                            ClaimValue = "PATCH",
                            RoleId = new Guid("8a21a41a-37d9-492a-a5f0-e568ccba1baa")
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "Tenants",
                            ClaimValue = "PUT",
                            RoleId = new Guid("8a21a41a-37d9-492a-a5f0-e568ccba1baa")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.PasswordIdentityConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.TenantIdentityConfigurationEntity", "TenantIdentityConfiguration")
                        .WithOne("PasswordIdentityConfiguration")
                        .HasForeignKey("AUTHIO.APPLICATION.Domain.Entities.PasswordIdentityConfigurationEntity", "TenantIdentityConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantIdentityConfiguration");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.RoleEntity", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.TenantEntity", "Tenant")
                        .WithMany("Roles")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.TenantEntity", "Tenant")
                        .WithOne("TenantConfiguration")
                        .HasForeignKey("AUTHIO.APPLICATION.Domain.Entities.TenantConfigurationEntity", "TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantIdentityConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.TenantConfigurationEntity", "TenantConfiguration")
                        .WithOne("TenantIdentityConfiguration")
                        .HasForeignKey("AUTHIO.APPLICATION.Domain.Entities.TenantIdentityConfigurationEntity", "TenantConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantConfiguration");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantIdentityUserAdminEntity", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.TenantEntity", "Tenant")
                        .WithMany("UserAdmins")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.TenantEntity", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.UserIdentityConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.TenantIdentityConfigurationEntity", "TenantIdentityConfiguration")
                        .WithOne("UserIdentityConfiguration")
                        .HasForeignKey("AUTHIO.APPLICATION.Domain.Entities.UserIdentityConfigurationEntity", "TenantIdentityConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantIdentityConfiguration");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.APPLICATION.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantConfigurationEntity", b =>
                {
                    b.Navigation("TenantIdentityConfiguration");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantEntity", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("TenantConfiguration");

                    b.Navigation("UserAdmins");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AUTHIO.APPLICATION.Domain.Entities.TenantIdentityConfigurationEntity", b =>
                {
                    b.Navigation("PasswordIdentityConfiguration");

                    b.Navigation("UserIdentityConfiguration");
                });
#pragma warning restore 612, 618
        }
    }
}
