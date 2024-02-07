using AUTHIO.API.Controllers.Base;
using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;

namespace AUTHIO.API.Controllers;

/// <summary>
/// Controller que cuida do fluxo de autenticação.
/// </summary>
/// <param name="featureFlags"></param>
/// <param name="unitOfWork"></param>
public class AuthenticationController(
    IFeatureFlags featureFlags, IUnitOfWork unitOfWork) 
        : BaseController(featureFlags, unitOfWork)
{

}
