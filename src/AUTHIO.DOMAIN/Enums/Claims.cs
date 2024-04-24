using System.ComponentModel;

namespace AUTHIO.DOMAIN.Enums
{
    public enum Claims
    {
        [Description("Accesso á Usuários.")]
        User = 1,
        [Description("Accesso á Tenants.")]
        Tenants = 2,
    }
}
