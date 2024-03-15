using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace AUTHIO.APPLICATION.Application.Configurations.Extensions.Initializers;

/// <summary>
/// Classe de configuração do Autenticação da aplicação.
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    /// Configuração da autenticação do sistema.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurations"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configurations)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            options.DefaultScheme = IdentityConstants.ApplicationScheme;

        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                LogValidationExceptions = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromHours(3),

                ValidIssuer = configurations.GetValue<string>("Auth:ValidIssuer"),
                ValidAudience = configurations.GetValue<string>("Auth:ValidAudience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configurations.GetValue<string>("Auth:SecurityKey")))
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Log.Error($"[LOG ERROR] {nameof(JwtBearerEvents)} - METHOD OnAuthenticationFailed - {context.Exception.Message}\n");

                    throw new Exception("Erro na autenticação");
                },
                OnTokenValidated = context =>
                {
                    Log.Information($"[LOG INFORMATION] {nameof(JwtBearerEvents)} - OnTokenValidated - {context.SecurityToken}\n");

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}
