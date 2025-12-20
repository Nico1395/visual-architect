using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Authentication.Domain.Constants;
using VisualArchitect.Api.Authentication.Presentation.Middleware;
using VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

namespace VisualArchitect.Api.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var gitHubClientId = configuration[AuthenticationConstants.Schemes.GitHub.Configuration.ClientId] ?? throw new MissingConfigurationException(AuthenticationConstants.Schemes.GitHub.Configuration.ClientId);
        var gitHubClientSecret = configuration[AuthenticationConstants.Schemes.GitHub.Configuration.ClientSecret] ?? throw new MissingConfigurationException(AuthenticationConstants.Schemes.GitHub.Configuration.ClientSecret);

        var clientBaseUrl = configuration[AuthenticationConstants.Configuration.ClientBaseUrl] ?? throw new MissingConfigurationException(AuthenticationConstants.Configuration.ClientBaseUrl);

        services.AddAntiforgery(options =>
        {
            options.HeaderName = AuthenticationConstants.Schemes.Antiforgery.HeaderName;
            options.Cookie.Name = AuthenticationConstants.Schemes.Antiforgery.Name;
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
        });
        services.AddTransient<CsrfMiddleware>();

        services.AddAuthentication(AuthenticationConstants.Schemes.Cookie.Scheme)
            .AddCookie(AuthenticationConstants.Schemes.Cookie.Scheme, options =>
            {
                options.Cookie.Name = AuthenticationConstants.Schemes.Cookie.Name;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = "/api/auth/login";
                options.LogoutPath = "/api/auth/logout";

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
            })
            .AddOAuth(AuthenticationConstants.Schemes.GitHub.Scheme, options =>
            {
                options.ClientId = gitHubClientId;
                options.ClientSecret = gitHubClientSecret;
                options.CallbackPath = new PathString("/api/auth/callback/github");

                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";

                options.Scope.Add("user:email");

                options.SaveTokens = true; // Tokens bleiben im AuthenticationTicket, serverseitig

                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, AuthenticationConstants.Schemes.GitHub.ClaimTypes.Id);
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, AuthenticationConstants.Schemes.GitHub.ClaimTypes.Login);
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, AuthenticationConstants.Schemes.GitHub.ClaimTypes.Email);

                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = OAuthEventHandler.HandleGitHubOnCreatingTicketAsync,
                };
            });

        services.AddAuthorization();

        services.AddCors(options =>
        {
            options.AddPolicy(AuthenticationConstants.Cors.VirtualArchitectClient.PolicyName, policy =>
            {
                policy
                    .WithOrigins(clientBaseUrl)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}
