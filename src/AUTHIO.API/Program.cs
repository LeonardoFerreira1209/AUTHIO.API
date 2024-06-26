using AUTHIO.APPLICATION.Configurations.Extensions.Initializers;
using AUTHIO.DOMAIN.Dtos.Configurations;
using AUTHIO.INFRASTRUCTURE.Middlewares;
using Hangfire;
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
        .ConfigureHangfire(configurations)
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

    applicationbuilder.UseHsts();
    applicationbuilder.UseHangfireDashboard();

    applicationbuilder.MapControllers();

    applicationbuilder
        .ConfigureServiceBusSubscriber();

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