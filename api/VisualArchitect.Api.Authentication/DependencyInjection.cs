using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
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
        var gitHubClientId = configuration["Authentication:GitHub:ClientId"] ?? throw new MissingConfigurationException("Authentication:GitHub:ClientId");
        var gitHubClientSecret = configuration["Authentication:GitHub:ClientSecret"] ?? throw new MissingConfigurationException("Authentication:GitHub:ClientSecret");

        services.AddAntiforgery(options =>
        {
            options.HeaderName = AuthenticationConstants.Schemes.Antiforgery.HeaderName;
            options.Cookie.Name = AuthenticationConstants.Schemes.Antiforgery.Name;
            options.Cookie.HttpOnly = false;
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

        services.AddCors(options =>
        {
            options.AddPolicy(AuthenticationConstants.Cors.VirtualArchitectClient.PolicyName, policy =>
            {
                policy
                    .WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}
