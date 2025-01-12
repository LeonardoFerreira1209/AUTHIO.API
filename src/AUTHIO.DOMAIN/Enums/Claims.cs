using System.ComponentModel;

namespace AUTHIO.DOMAIN.Enums
{
    public enum Claims
    {
        [Description("Accesso á Usuários.")]
        User = 1,
        [Description("Accesso á Clients.")]
        Clients = 2,
    }
}
