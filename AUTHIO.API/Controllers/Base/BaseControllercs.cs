using AUTHIO.APPLICATION.Domain.Contracts.Repository;
using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Domain.Entity;
using AUTHIO.APPLICATION.Domain.Enums;
using AUTHIO.APPLICATION.Domain.Exceptions.Base;
using AUTHIO.APPLICATION.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AUTHIO.API.Controllers.Base;

/// <summary>
/// Controller base
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="featureFlags"></param>
/// <param name=""></param>
public class BaseControllercs(
    IFeatureFlags featureFlags, IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IFeatureFlags _featureFlags = featureFlags;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <summary>
    /// Método que verifica se o endpoint está ativado.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodName"></param>
    /// <param name="method"></param>
    /// <param name="methodDescription"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    protected async Task<T> ExecuteAsync<T>(string methodName, Func<Task<T>> method, string methodDescription)
    {
        var featureFlag
            = await _featureFlags.GetFeatureDefinitionAsync(methodName)
            ?? await _featureFlags.CreateAsync(new FeatureFlagsEntity
            {
                Name = methodName,
                Created = DateTime.UtcNow,
                IsEnabled = true,
                Status = Status.Ativo,

            }).ContinueWith(async (taskResult) =>
            {
                await _unitOfWork.CommitAsync();

                return taskResult.Result;

            }).Result;

        if (featureFlag.IsEnabled is false)
            throw new CustomException(HttpStatusCode.NotImplemented, null, [
                new($"Método {methodName} inativado!")
            ]);

        return await Tracker.Time(
            () => method(), methodDescription);
    }
}
