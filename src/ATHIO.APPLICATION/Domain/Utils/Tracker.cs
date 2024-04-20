using Serilog;
using System.Diagnostics;

namespace AUTHIO.APPLICATION.Domain.Utils;

/// <summary>
/// Tracker para chamadas de funções.
/// </summary>
public static class Tracker
{
    /// <summary>
    /// Faz a contagem de inicio e fim de métodos Task.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static async Task Time(Func<Task> method, string message)
    {
        var time = new Stopwatch();

        time.Start();

        await method();

        time.Stop();

        Log.Information($"[LOG INFORMATION] - {message}, Tempo: {time.Elapsed}\n");
    }

    /// <summary>
    /// Faz a contagem de inicio e fim de métodos Task com retorno.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static async Task<T> Time<T>(Func<Task<T>> method, string message)
    {
        var time = new Stopwatch();

        time.Start();

        var result = await method();

        time.Stop();

        Log.Information($"[LOG INFORMATION] - {message}, Tempo: {time.Elapsed}\n");

        return result;
    }
}

