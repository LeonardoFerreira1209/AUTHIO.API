using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AUTHIO.APPLICATION.Domain.Contracts.Repositories.Base;

public interface IUnitOfWork<TContext> where TContext : DbContext
{
    /// <summary>
    /// Commita a transação.
    /// </summary>
    /// <returns></returns>
    Task CommitAsync();

    /// <summary>
    /// Reverte a transação.
    /// </summary>
    /// <returns></returns>
    Task RollbackAsync();

    /// <summary>
    /// Começar transação.
    /// </summary>
    /// <returns></returns>
    Task<IDbContextTransaction> BeginTransactAsync();

    /// <summary>
    /// Finalizar transação.
    /// </summary>
    /// <returns></returns>
    Task CommitTransactAsync(IDbContextTransaction dbContextTransaction);

    /// <summary>
    /// Resetar transação.
    /// </summary>
    /// <returns></returns>
    Task RollBackTransactionAsync(IDbContextTransaction dbContextTransaction);

    /// <summary>
    /// Abrir uma conexão com o banco de dados.
    /// </summary>
    /// <returns></returns>
    Task OpenConnectAsync();

    /// <summary>
    /// Fechar uma conexão com o banco de dados.
    /// </summary>
    /// <returns></returns>
    Task CloseConnectionAsync();
}
