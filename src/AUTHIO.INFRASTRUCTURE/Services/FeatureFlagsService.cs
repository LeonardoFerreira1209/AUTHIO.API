using AUTHIO.DATABASE.Context;
using AUTHIO.DOMAIN.Contracts.Repositories;
using AUTHIO.DOMAIN.Contracts.Repositories.Base;
using AUTHIO.DOMAIN.Contracts.Services;
using AUTHIO.DOMAIN.Entities;
using AUTHIO.DOMAIN.Enums;
using AUTHIO.DOMAIN.Exceptions.Base;
using AUTHIO.DOMAIN.Helpers;
using System.Net;

namespace AUTHIO.INFRASTRUCTURE.Services;

/// <summary>
/// Serviços de flags do sistema.
/// </summary>
/// <param name="featureFlags"></param>
/// <param name="unitOfWork"></param>
public class FeatureFlagsService(
    IFeatureFlagsRepository featureFlags, IUnitOfWork<AuthIoContext> unitOfWork) : IFeatureFlagsService
{
    private readonly IFeatureFlagsRepository _featureFlags = featureFlags;
    private readonly IUnitOfWork<AuthIoContext> _unitOfWork = unitOfWork;

    /// <summary>
    /// Verifica se endponint está cadastro, caso esteja verifica se esta ativo, se não cadastra.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="methodName"></param>
    /// <param name="method"></param>
    /// <param name="methodDescription"></param>
    /// <returns></returns>
    /// <exception cref="CustomException"></exception>
    public async Task<T> ExecuteAsync<T>(string methodName, Func<Task<T>> method, string methodDescription)
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

        if (featureFlag.IsEnabled is false || featureFlag.Status is Status.Inativo)
            throw new CustomException(HttpStatusCode.NotImplemented, null, [
                new($"Método {methodName} inativado!")
            ]);

        return await Tracker.Time(
            () => method(), $"{methodName} - {methodDescription}");
    }
}