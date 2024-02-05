using AUTHIO.APPLICATION.Domain.Entity;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;

public interface IGenerictEntityCoreRepository<T> : IGenericRepository<T> where T : class, IEntityBase
{
    /// <summary>
    /// Criar vários.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<IList<T>> BulkInsertAsync(IList<T> entities);

    /// <summary>
    /// Atualizar vários.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<IList<T>> BulkUpdateAsync(IList<T> entities);

    /// <summary>
    /// Deletar vários.
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task BulkDeleteAsync(IList<T> entities);
}
