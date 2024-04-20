using System.ComponentModel;

namespace AUTHIO.APPLICATION.DOMAIN.ENUMS
{
    public enum Claims
    {
        [Description("Accesso á Usuários.")]
        User = 1,
        [Description("Accesso á Tenants.")]
        Tenants = 2,
    }
}
