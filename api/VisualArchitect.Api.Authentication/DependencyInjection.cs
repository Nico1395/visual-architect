using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Authentication.Presentation.Middleware;
using VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

namespace VisualArchitect.Api.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var gitHubClientId = configuration["Authentication:GitHub:ClientId"] ?? throw new MissingConfigurationException("Authentication:GitHub:ClientId");
        var gitHubClientSecret = configuration["Authentication:GitHub:ClientSecret"] ?? throw new MissingConfigurationException("Authentication:GitHub:ClientSecret");

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
            })
            .AddOAuth("GitHub", options =>
            {
                options.ClientId = gitHubClientId;
                options.ClientSecret = gitHubClientSecret;
                options.CallbackPath = new PathString("/api/auth/callback/github");

                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";

                options.Scope.Add("user:email");

                options.SaveTokens = true; // Tokens bleiben im AuthenticationTicket, serverseitig

                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        // GitHub UserInfo abrufen
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        using var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                        context.RunClaimActions(user.RootElement);
                    }
                };
            });

        services.AddAuthorization();

        return services;
    }
}
