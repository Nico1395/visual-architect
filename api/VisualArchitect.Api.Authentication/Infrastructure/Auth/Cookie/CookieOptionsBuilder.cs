using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VisualArchitect.Api.Authentication.Domain.Constants;

namespace VisualArchitect.Api.Authentication.Infrastructure.Auth.Cookie;

public static class CookieOptionsBuilder
{
    public static void Build(CookieAuthenticationOptions options, IConfiguration configuration)
    {
        options.Cookie.Name = AuthenticationConstants.Schemes.Cookie.Name;
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
        options.LoginPath = "/api/v1/auth/login";
        options.LogoutPath = "/api/v1/auth/logout";

        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/auth"))
                {
                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                }

                ctx.Response.Redirect(ctx.RedirectUri);
                return Task.CompletedTask;
            }
        };
    }
}
