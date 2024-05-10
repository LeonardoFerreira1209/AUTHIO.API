﻿// <auto-generated />
using System;
using AUTHIO.DATABASE.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AUTHIO.INFRASTRUCTURE.Migrations
{
    [DbContext(typeof(AuthIoContext))]
    partial class AuthIoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.EventEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("JsonBody")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Processed")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("SchedulerTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Sended")
                        .HasColumnType("datetime(6)");
                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.FeatureFlagsEntity", b =>
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

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.LockoutIdentityConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("AllowedForNewUsers")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeSpan>("DefaultLockoutTimeSpan")
                        .HasColumnType("time(6)");

                    b.Property<int>("MaxFailedAccessAttempts")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantIdentityConfigurationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TenantIdentityConfigurationId")
                        .IsUnique();

                    b.ToTable("LockoutIdentityConfigurations", (string)null);
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.PasswordIdentityConfigurationEntity", b =>
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

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.RoleEntity", b =>
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
                            Id = new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7"),
                            Created = new DateTime(2024, 5, 10, 1, 31, 50, 355, DateTimeKind.Local).AddTicks(4511),
                            Id = new Guid("278d2723-54f5-43b3-b145-eaff88575613"),
                            Created = new DateTime(2024, 5, 8, 0, 22, 55, 299, DateTimeKind.Local).AddTicks(5798),
                            Name = "System",
                            NormalizedName = "SYSTEM",
                            Status = 1,
                            System = true
                        });
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TenantKey")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId")
                        .IsUnique();

                    b.ToTable("TenantConfigurations");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantEmailConfigurationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SendersEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("SendersName")
                        .HasColumnType("longtext");

                    b.Property<Guid>("TenantConfigurationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TenantConfigurationId")
                        .IsUnique();

                    b.ToTable("TenantEmailConfigurations");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantEntity", b =>
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

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantIdentityConfigurationEntity", b =>
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

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantIdentityUserAdminEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId", "TenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantUserAdmins", (string)null);
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.UserEntity", b =>
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

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.UserIdentityConfigurationEntity", b =>
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
                            RoleId = new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7")
                            RoleId = new Guid("278d2723-54f5-43b3-b145-eaff88575613")
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "Tenants",
                            ClaimValue = "GET",
                            RoleId = new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7")
                            RoleId = new Guid("278d2723-54f5-43b3-b145-eaff88575613")
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "Tenants",
                            ClaimValue = "PATCH",
                            RoleId = new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7")
                            RoleId = new Guid("278d2723-54f5-43b3-b145-eaff88575613")
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "Tenants",
                            ClaimValue = "PUT",
                            RoleId = new Guid("063553c5-f359-4e0e-94e7-23e6f54d5fd7")
                            RoleId = new Guid("278d2723-54f5-43b3-b145-eaff88575613")
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

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.LockoutIdentityConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantIdentityConfigurationEntity", "TenantIdentityConfiguration")
                        .WithOne("LockoutIdentityConfiguration")
                        .HasForeignKey("AUTHIO.DOMAIN.Entities.LockoutIdentityConfigurationEntity", "TenantIdentityConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantIdentityConfiguration");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.PasswordIdentityConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantIdentityConfigurationEntity", "TenantIdentityConfiguration")
                        .WithOne("PasswordIdentityConfiguration")
                        .HasForeignKey("AUTHIO.DOMAIN.Entities.PasswordIdentityConfigurationEntity", "TenantIdentityConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantIdentityConfiguration");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.RoleEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantEntity", "Tenant")
                        .WithMany("Roles")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantEntity", "Tenant")
                        .WithOne("TenantConfiguration")
                        .HasForeignKey("AUTHIO.DOMAIN.Entities.TenantConfigurationEntity", "TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantEmailConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantConfigurationEntity", "TenantConfiguration")
                        .WithOne("TenantEmailConfiguration")
                        .HasForeignKey("AUTHIO.DOMAIN.Entities.TenantEmailConfigurationEntity", "TenantConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantConfiguration");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantIdentityConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantConfigurationEntity", "TenantConfiguration")
                        .WithOne("TenantIdentityConfiguration")
                        .HasForeignKey("AUTHIO.DOMAIN.Entities.TenantIdentityConfigurationEntity", "TenantConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantConfiguration");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantIdentityUserAdminEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantEntity", "Tenant")
                        .WithMany("UserAdmins")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AUTHIO.DOMAIN.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.UserEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantEntity", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.UserIdentityConfigurationEntity", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.TenantIdentityConfigurationEntity", "TenantIdentityConfiguration")
                        .WithOne("UserIdentityConfiguration")
                        .HasForeignKey("AUTHIO.DOMAIN.Entities.UserIdentityConfigurationEntity", "TenantIdentityConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TenantIdentityConfiguration");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AUTHIO.DOMAIN.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("AUTHIO.DOMAIN.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantConfigurationEntity", b =>
                {
                    b.Navigation("TenantEmailConfiguration");

                    b.Navigation("TenantIdentityConfiguration");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantEntity", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("TenantConfiguration");

                    b.Navigation("UserAdmins");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AUTHIO.DOMAIN.Entities.TenantIdentityConfigurationEntity", b =>
                {
                    b.Navigation("LockoutIdentityConfiguration");

                    b.Navigation("PasswordIdentityConfiguration");

                    b.Navigation("UserIdentityConfiguration");
                });
#pragma warning restore 612, 618
        }
    }
}
