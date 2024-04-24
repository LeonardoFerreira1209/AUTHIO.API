using System.Security.Cryptography;

namespace AUTHIO.DOMAIN.Helpers.Extensions;

/// <summary>
/// Classe de Base32
/// </summary>
public static class Base32
{
    private const string _base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

    /// <summary>
    /// Gera um base 32.
    /// </summary>
    /// <returns></returns>
    public static string GenerateBase32()
    {
        const int length = 20;
        return string.Create((length + 4) / 5 * 8, 0, static (buffer, _) =>
        {
            Span<byte> bytes = stackalloc byte[length];
            RandomNumberGenerator.Fill(bytes);

            var index = 0;
            for (int offset = 0; offset < bytes.Length;)
            {
                int numCharsToOutput = GetNextGroup(bytes, ref offset, out byte a, out byte b, out byte c,
                    out byte d, out byte e, out byte f, out byte g, out byte h);

                buffer[index + 7] = numCharsToOutput >= 8 ? _base32Chars[h] : '=';
                buffer[index + 6] = numCharsToOutput >= 7 ? _base32Chars[g] : '=';
                buffer[index + 5] = numCharsToOutput >= 6 ? _base32Chars[f] : '=';
                buffer[index + 4] = numCharsToOutput >= 5 ? _base32Chars[e] : '=';
                buffer[index + 3] = numCharsToOutput >= 4 ? _base32Chars[d] : '=';
                buffer[index + 2] = numCharsToOutput >= 3 ? _base32Chars[c] : '=';
                buffer[index + 1] = numCharsToOutput >= 2 ? _base32Chars[b] : '=';
                buffer[index] = numCharsToOutput >= 1 ? _base32Chars[a] : '=';
                index += 8;
            }
        });
    }

    /// <summary>
    /// Retorna o próximo grupo.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="offset"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <param name="f"></param>
    /// <param name="g"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    private static int GetNextGroup(Span<byte> input, ref int offset, out byte a,
        out byte b, out byte c, out byte d, out byte e, out byte f, out byte g, out byte h)
    {
        uint b1, b2, b3, b4, b5;

        var retVal = (input.Length - offset) switch
        {
            1 => 2,
            2 => 4,
            3 => 5,
            4 => 7,
            _ => 8,
        };

        b1 = offset < input.Length ? input[offset++] : 0U;
        b2 = offset < input.Length ? input[offset++] : 0U;
        b3 = offset < input.Length ? input[offset++] : 0U;
        b4 = offset < input.Length ? input[offset++] : 0U;
        b5 = offset < input.Length ? input[offset++] : 0U;

        a = (byte)(b1 >> 3);
        b = (byte)((b1 & 0x07) << 2 | b2 >> 6);
        c = (byte)(b2 >> 1 & 0x1f);
        d = (byte)((b2 & 0x01) << 4 | b3 >> 4);
        e = (byte)((b3 & 0x0f) << 1 | b4 >> 7);
        f = (byte)(b4 >> 2 & 0x1f);
        g = (byte)((b4 & 0x3) << 3 | b5 >> 5);
        h = (byte)(b5 & 0x1f);

        return retVal;
    }
}
