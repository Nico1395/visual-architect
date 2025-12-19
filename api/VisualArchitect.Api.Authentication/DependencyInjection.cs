using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Authentication.Presentation.Middleware;

namespace VisualArchitect.Api.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectAuthentication(this IServiceCollection services)
    {
        services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-CSRF-TOKEN";
            options.Cookie.Name = "vac_csrf";
            options.Cookie.HttpOnly = false;
        });
        services.AddTransient<CsrfMiddleware>();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = "vac";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
            });

        services.AddAuthorization();

        return services;
    }
}
