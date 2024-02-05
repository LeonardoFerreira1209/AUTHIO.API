using AUTHIO.APPLICATION.Domain.Contracts.Repository.Base;
using AUTHIO.APPLICATION.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AUTHIO.APPLICATION.Infra.Repository.Base;

/// <summary>
/// Unidade de trabalho de banco de daods e controle de transações.
/// </summary>
/// <remarks>
/// ctor.
/// </remarks>
/// <param name="context"></param>
public class UnitOfWork(
    AuthIoContext context) : IUnitOfWork
{
    private readonly AuthIoContext _context = context;

    /// <summary>
    /// Comita a transação.
    /// </summary>
    /// <returns></returns>
    public async Task CommitAsync()
        => await _context.SaveChangesAsync();

    /// <summary>
    /// Rollback a transação.
    /// </summary>
    /// <returns></returns>
    public async Task RollbackAsync()
        => await _context.DisposeAsync();

    /// <summary>
    /// Começar uma transação no banco de dados.
    /// </summary>
    /// <returns></returns>
    public async Task<IDbContextTransaction> BeginTransactAsync()
        => await _context.Database.BeginTransactionAsync();

    /// <summary>
    /// Fechar uma conexão com o banco de dados.
    /// </summary>
    /// <returns></returns>
    public async Task CloseConnectionAsync()
        => await _context.Database.CloseConnectionAsync();

    /// <summary>
    /// Commitar e finalizar uma transação no banco de dados.
    /// </summary>
    /// <returns></returns>
    public async Task CommitTransactAsync(IDbContextTransaction dbContextTransaction)
        => await dbContextTransaction.CommitAsync();

    /// <summary>
    /// Abrir uma conexão com o banco de dados.
    /// </summary>
    /// <returns></returns>
    public async Task OpenConnectAsync()
        => await _context.Database.OpenConnectionAsync();

    /// <summary>
    /// Resetar uma transação.
    /// </summary>
    /// <returns></returns>
    public async Task RollBackTransactionAsync(IDbContextTransaction dbContextTransaction)
        => await dbContextTransaction.RollbackAsync();
}
