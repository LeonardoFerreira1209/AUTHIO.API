﻿using AUTHIO.DOMAIN.Dtos.Configurations;
using Dapper;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data;

namespace AUTHIO.INFRASTRUCTURE.Repositories.Base;

public class BaseRepository
{
    public int _commandTimeout { get; set; }

    private readonly IOptions<AppSettings> _configuracoes;

    public BaseRepository(IOptions<AppSettings> configuracoes)
    {
        _configuracoes = configuracoes;

        //_commandTimeout = configuracoes.Value.Configuracoes.TimeOutDefault;

        if (_commandTimeout == 0) _commandTimeout = 900;
    }

    protected IDbConnection GerarConexaoConnect() => new MySqlConnection(_configuracoes.Value.ConnectionStrings.DataBase);

    public virtual async Task<IEnumerable<T>> DbQueryAsync<T>(IDbConnection dbCon, string sql, object parameters = null)
    {
        return await dbCon.QueryAsync<T>(sql, parameters, commandTimeout: _commandTimeout);
    }

    public virtual async Task<T> DbQuerySingleAsync<T>(IDbConnection dbCon, string sql, object parameters)
    {
        return await dbCon.QueryFirstOrDefaultAsync<T>(sql, parameters, commandTimeout: _commandTimeout);
    }

    public virtual async Task<bool> DbExecuteAsync<T>(IDbConnection dbCon, string sql, object parameters, CommandType commandType = CommandType.Text)
    {
        return await dbCon.ExecuteAsync(sql, parameters, commandTimeout: _commandTimeout, commandType: commandType) > 0;
    }

    public virtual async Task<bool> DbExecuteScalarAsync(IDbConnection dbCon, string sql, object parameters)
    {
        return await dbCon.ExecuteScalarAsync<bool>(sql, parameters, commandTimeout: _commandTimeout);
    }

    public virtual async Task<T> DbExecuteScalarDynamicAsync<T>(IDbConnection dbCon, string sql, object parameters = null)
    {
        return await dbCon.ExecuteScalarAsync<T>(sql, parameters, commandTimeout: _commandTimeout);
    }
}
