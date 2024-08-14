using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace AUTHIO.DOMAIN.Auth;

public class JwtPostConfigureOptions(
    IServiceProvider serviceProvider) : IPostConfigureOptions<JwtBearerOptions>
{
    public void PostConfigure(
        string name, JwtBearerOptions options)
    {
        options.TokenHandlers.Clear();
        options.TokenHandlers.Add(new JwtServiceValidationHandler(
            serviceProvider 
            )
        );
    }
}