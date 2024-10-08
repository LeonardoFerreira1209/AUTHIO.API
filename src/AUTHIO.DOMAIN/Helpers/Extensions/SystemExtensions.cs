﻿using AUTHIO.DOMAIN.Dtos.Response.Base;
using AUTHIO.DOMAIN.Enums;
using FluentValidation.Results;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

public static class SystemExtensions
{
    private static T ObterAtributoDoTipo<T>(this Enum valorEnum) where T : Attribute
    {
        var type = valorEnum.GetType();

        var memInfo = type.GetMember(valorEnum.ToString());

        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);

        return attributes.Length > 0 ? (T)attributes[0] : null;
    }

    /// <summary>
    /// Obtem a descrição de um Enum.
    /// </summary>
    /// <param name="valorEnum"></param>
    /// <returns></returns>
    public static string ObterDescricao(this Enum valorEnum)
    {
        return valorEnum.ObterAtributoDoTipo<DescriptionAttribute>().Description;
    }

    /// <summary>
    /// Obtem o código de um Enum
    /// </summary>
    /// <param name="valor"></param>
    /// <returns></returns>
    public static string ToCode(this ErrorCode valor)
    {
        return valor.GetHashCode().ToString();
    }

    /// <summary>
    /// Obtém a data atual referente a Brasilia.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static DateTime DataAtualBrasilia(this DateTime data)
    {
        DateTime dateTime = DateTime.UtcNow;

        TimeZoneInfo hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, hrBrasilia);
    }

    /// <summary>
    /// Serializa objeto ignorando objetos nulos.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string SerializeIgnoreNullValues(this object item)
    {
        var jsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        return JsonConvert.SerializeObject(item, jsonSerializerSettings);
    }

    /// <summary>
    /// Retorna uma lista de DadosNotificacao.
    /// </summary>
    /// <param name="resultado"></param>
    /// <returns></returns>
    public static List<DataNotifications> GetErros(this ValidationResult resultado)
    {
        var erros = new List<DataNotifications>();

        foreach (var erro in resultado.Errors)
        {
            erros.Add(new DataNotifications(erro.ErrorMessage));
        }

        return erros;
    }

    /// <summary>
    /// Convert DateTime to DateOnly
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static DateOnly ToDateOnly(this DateTime dateTime)
    {
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }

    /// <summary>
    /// Conver DateTime to TimeUnly
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static TimeOnly ToTimeOnly(this DateTime dateTime)
    {
        return new TimeOnly(dateTime.Hour, dateTime.Minute);
    }

    /// <summary>
    /// Convert DateOnly to DateTime
    /// </summary>
    /// <param name="dateOnly"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this DateOnly dateOnly)
    {
        return new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
    }

    /// <summary>
    /// Convert TimeOnly to DateTime
    /// </summary>
    /// <param name="timeOnly"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this TimeOnly timeOnly)
    {
        return new DateTime(0001, 01, 01, timeOnly.Hour, timeOnly.Minute, timeOnly.Second);
    }

    /// <summary>
    /// Verifica se o tipo de arquivo é aceito.
    /// </summary>
    /// <param name="fileType"></param>
    /// <returns></returns>
    public static bool FileTypesAllowed(this string fileType)
    {
        var typesAllowed = new List<string> { "image/jpeg", "image/jpg", "image/png", "image/gif" };

        return typesAllowed.Select(types => types.Equals(fileType)).Any(verify => verify is true);
    }

    /// <summary>
    /// Normaliza um texto removendo os acentos.
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string RemoveAccentAndConvertToLower(this string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();
    }

    /// <summary>
    /// Transforma código em números
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string HashCode(this string code)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(code));

        StringBuilder builder = new();

        for (int i = 0; i < bytes.Length; i++)
            builder.Append(bytes[i].ToString("X2"));

        return builder.ToString().ConvertToNumeric().TruncateNumericCode();
    }

    /// <summary>
    /// Converte um hashCode para string numérica.
    /// </summary>
    /// <param name="hashedCode"></param>
    /// <returns></returns>
    private static long ConvertToNumeric(this string hashedCode)
    {
        long numericCode = 0;

        foreach (char c in hashedCode)
            numericCode += c;

        return numericCode * new Random().NextInt64(1, 9999);
    }

    /// <summary>
    /// Trunca um código numérico
    /// </summary>
    /// <param name="numericCode"></param>
    /// <returns></returns>
    private static string TruncateNumericCode(this long numericCode)
    {
        string limitedCode = numericCode.ToString();

        if (limitedCode.Length > 4)
        {
            limitedCode = limitedCode.Substring(0, 4);
        }

        return limitedCode;
    }
}

