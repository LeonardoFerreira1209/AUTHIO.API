﻿using AUTHIO.APPLICATION.Configurations.Extensions.Initializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;

namespace AUTHIO.APPLICATION.Configurations.Extensions.Initializers;

// <summary>
/// Classe de configuração do HealthChecks da aplicação.
/// </summary>
public static class HealthCheckExtensions
{
    private static readonly string HealthCheckEndpoint = "/application/healthcheck";
    private static readonly string[] tags = ["Core", "MySql"];

    /// <summary>
    /// Configuração do HealthChecks do sistema.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurations"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configurations)
    {
        string connectionString = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? configurations
                    .GetConnectionString("DataBase");

        services
           .AddHealthChecks().AddMySql(connectionString, name: "Base de dados padrão.", tags: tags);

        return services;
    }

    /// <summary>
    /// Configuração do HealthChecks do sistema.
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder application)
        => application.UseHealthChecks(HealthCheckEndpoint, new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                var result = JsonConvert.SerializeObject(new
                {
                    statusApplication = report.Status.ToString(),

                    healthChecks = report.Entries.Select(e => new
                    {
                        check = e.Key,
                        ErrorMessage = e.Value.Exception?.Message,
                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                    })
                });

                context.Response.ContentType = MediaTypeNames.Application.Json;

                await context.Response.WriteAsync(result);
            }
        });
}
