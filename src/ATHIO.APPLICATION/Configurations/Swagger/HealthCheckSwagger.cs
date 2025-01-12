using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AUTHIO.APPLICATION.Configurations.Swagger;

/// <summary>
/// Classe de configuração do healthCheck da aplicação.
/// </summary>
public class HealthCheckSwagger : IDocumentFilter
{
    public static readonly string HealthCheckEndpoint = "/application/healthcheck";

    /// <summary>
    /// Configuração do HelthCheck.
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathItem = new OpenApiPathItem();

        pathItem.Operations.Add(OperationType.Get, new OpenApiOperation
        {
            OperationId = "HeathCheck",
            Tags = [new OpenApiTag { Name = "Health-Check" }],
            Responses = new OpenApiResponses
            {
                ["200"] = new OpenApiResponse { Description = "Healthy" },
                ["503"] = new OpenApiResponse { Description = "Unhealthy" }
            }
        });

        swaggerDoc.Paths.Add(HealthCheckEndpoint, pathItem);
    }
}
