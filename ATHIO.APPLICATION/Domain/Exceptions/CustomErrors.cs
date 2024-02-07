namespace AUTHIO.APPLICATION.Domain.Exceptions;

public static class CustomErrors
{
    /// <summary>
    /// Retorna exceptions customizadas.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string CustomExceptionMessage(this string code)
    {
        switch (code)
        {
            case "DuplicateUserName":
                return "O usuário informado já foi cadastrado.";
            case "DuplicateEmail":
                return "O email informado já foi usado.";
            case "PasswordTooShort":
                return "A senha deve ter no mínimo 12 caracteres.";
            case "PasswordRequiresNonAlphanumeric":
                return "A senha deve conter pelo menos um caractere não alfanumérico.";
            case "PasswordRequiresDigit":
                return "A senha deve conter pelo menos um dígito (0-9).";
            case "PasswordRequiresUpper":
                return "A senha deve conter pelo menos uma letra maiúscula (A-Z).";
            case "PasswordRequiresLower":
                return "A senha deve conter pelo menos uma letra minúscula (a-z).";
            case "PasswordMismatch":
                return "A senha atual está incorreta.";
            case "UserAlreadyInRole":
                return "O usuário já está vinculado à role informada.";
            default:
                return "Erro não tratado no servidor.";
        }
    }
}
