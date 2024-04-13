﻿using AUTHIO.APPLICATION.Domain.Entities;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;

public interface IGenerictEntityCoreRepository<T> : IGenericRepository<T> where T : class, IEntityPrimaryKey<Guid>
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
