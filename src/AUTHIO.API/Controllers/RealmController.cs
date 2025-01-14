using AUTHIO.API.Controllers.Base;
using AUTHIO.DOMAIN.Auth.CustomAuthorize.Attribute;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Contracts.Services.Infrastructure;
using AUTHIO.DOMAIN.Dtos.Request;
using AUTHIO.DOMAIN.Dtos.Request.Base;
using AUTHIO.DOMAIN.Dtos.Response;
using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Newtonsoft.Json;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;

namespace AUTHIO.API.Controllers;

/// <summary>
/// Controller que cuida do fluxo de realms.
/// </summary>
/// <param name="featureFlags"></param>
/// <param name="realmService"></param>
[ApiController]
[ControllerName("Realms")]
[Route("api/realms")]
public class RealmController(
    IFeatureFlagsService featureFlags,
    IRealmService realmService) : BaseController(featureFlags)
{
    /// <summary>
    /// Endpoint responsável pelo registro de Realm.
    /// </summary>
    /// <param name="createRealmRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Realms, "POST")]
    [SwaggerOperation(Summary = "Registrar Realm", Description = "Método responsável por registrar um novo Realm!")]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateRealmRequest createRealmRequest,
        CancellationToken cancellationToken
        )
    {
        using (LogContext.PushProperty("Controller", nameof(RealmController)))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(createRealmRequest)))
        using (LogContext.PushProperty("Metodo", nameof(CreateAsync)))
        {
            return await ExecuteAsync(
                nameof(CreateAsync),
                () => realmService.CreateAsync(
                    createRealmRequest,
                    cancellationToken
                ),
                "Registrar Client.",
                cancellationToken
            );
        }
    }

    /// <summary>
    /// Endpoint responsável pela atualização de um Client.
    /// </summary>
    /// <param name="updateRealmRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Realms, "PUT")]
    [SwaggerOperation(
        Summary = "Atualizar Realm",
        Description = "Método responsável por atualizar um Realm!"
    )]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateRealmRequest updateRealmRequest,
        CancellationToken cancellationToken
        )
    {
        using (LogContext.PushProperty("Controller", nameof(RealmController)))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(updateRealmRequest)))
        using (LogContext.PushProperty("Metodo", nameof(UpdateAsync)))
        {
            return await ExecuteAsync(
                nameof(UpdateAsync),
                () => realmService.UpdateAsync(
                    updateRealmRequest,
                    cancellationToken
                ),
                "Atualizar Realm.",
                cancellationToken
            );
        }
    }

    /// <summary>
    /// Endpoint responsável por buscar os Clients do usuário logado.
    /// </summary>
    /// <param name="filterRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Realms, "GET")]
    [SwaggerOperation(
        Summary = "Buscar todos os Realms",
        Description = "Método responsável por buscar todos os Reamls do usuário!"
    )]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllAsync(
           [FromQuery] FilterRequest filterRequest,
           CancellationToken cancellationToken
        )
    {
        using (LogContext.PushProperty("Controller", nameof(RealmController)))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(filterRequest)))
        using (LogContext.PushProperty("Metodo", nameof(GetAllAsync)))
        {
            return await ExecuteAsync(
                nameof(GetAllAsync),
                () => realmService.GetAllAsync(
                    filterRequest,
                    cancellationToken
                ),
                "Buscar todos os Realms.",
                cancellationToken
            );
        }
    }

    /// <summary>
    /// Endpoint responsável pelo registro de client em um Realm.
    /// </summary>
    /// <param name="createClientRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("{realmId}/clients/register")]
    [EnableRateLimiting("default-fixed-window")]
    [Authorize(Claims.Realms, "PATCH")]
    [SwaggerOperation(
        Summary = "Registrar client no Realm",
        Description = "Método responsável por registrar um client no Realm!"
    )]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ClientResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterRealmClientAsync(
        Guid realmId,
        [FromBody] CreateClientRequest createClientRequest,
        CancellationToken cancellationToken)
    {
        using (LogContext.PushProperty("Controller", nameof(RealmController)))
        using (LogContext.PushProperty("Payload", JsonConvert.SerializeObject(createClientRequest)))
        using (LogContext.PushProperty("Metodo", nameof(RegisterRealmClientAsync)))
        {
            return await ExecuteAsync(
                nameof(RegisterRealmClientAsync),
                () => realmService.RegisterRealmClientAsync(
                    createClientRequest,
                    realmId,
                    cancellationToken
                ),
                "Registrar Client no Realm.",
                cancellationToken
            );
        }
    }
}
