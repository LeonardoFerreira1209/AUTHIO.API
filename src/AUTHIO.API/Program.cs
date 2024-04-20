using AUTHIO.APPLICATION.Application.Configurations.Extensions.Initializers;
using AUTHIO.APPLICATION.Domain.Dtos.Configurations;
using AUTHIO.APPLICATION.Infra.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var configurations = builder.Configuration;

    /// <sumary>
    /// Pega o appsettings baseado no ambiente em execução.
    /// </sumary>
    configurations
         .SetBasePath(builder.Environment.ContentRootPath)
            .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();

    configurations
        .SetBasePath(builder.Environment.ContentRootPath)
           .AddJsonFile("identitysettings.json", true, true)
                   .AddEnvironmentVariables();

    /// <sumary>
    /// Configura as configurações de inicialização da aplicação.
    /// </sumary>
    builder.Services
        .ConfigureSerilog(configurations)
        .AddHttpContextAccessor()
        .Configure<AppSettings>(configurations)
        .AddSingleton<AppSettings>()
        .AddEndpointsApiExplorer()
        .AddOptions()
        .ConfigureLanguage()
        .ConfigureFixedRateLimit()
        .ConfigureDependencies(configurations)
        .ConfigureDatabase(configurations)
        .ConfigureIdentityServer(configurations)
        .AddAuthorization()
        .ConfigureAuthentication(configurations)
        .ConfigureApplicationCookie()
        .ConfigureHealthChecks(configurations)
        .ConfigureCors()
        .AddControllers(options =>
        {
            options.EnableEndpointRouting = false;
            options.Filters.Add(new ProducesAttribute("application/json"));

        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });


    builder.Services
           .ConfigureSwagger(configurations);

    /// <sumary>
    /// Configura as configurações de inicialização que só devem ser usadas em Produção.
    /// </sumary>
    //if (builder.Environment.IsProduction())
    //    builder.Services
    //        .ConfigureTelemetry(configurations)
    //            .ConfigureApplicationInsights(configurations);

    var applicationbuilder = builder.Build();

    applicationbuilder
        .UseMiddleware<ErrorHandlerMiddleware>()
        .UseHttpsRedirection()
        .UseRateLimiter()
        .UseDefaultFiles()
        .UseStaticFiles()
        .UseCookiePolicy()
        .UseCors()
        .UseResponseCaching()
        .UseAuthorization()
        .UseAuthentication()
        .UseHealthChecks()
        .UseSwaggerConfigurations(configurations);

    if (applicationbuilder.Environment.IsProduction())
        applicationbuilder.UseHsts();

    applicationbuilder.MapControllers();

    applicationbuilder
       .Lifetime.ApplicationStarted
           .Register(() => Log.Debug(
                   $"[LOG DEBUG] - Aplicação inicializada com sucesso: [AUTHIO.API]\n"));

    applicationbuilder.Run();
}
catch (Exception exception)
{
    Log.Error($"[LOG ERROR] - Ocorreu um erro ao inicializar a aplicacao [AUTHIO.API] - {exception.Message}\n"); throw;
}