using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Authentication.Domain.Constants;
using VisualArchitect.Api.Authentication.Infrastructure.Auth.Cookie;
using VisualArchitect.Api.Authentication.Infrastructure.Auth.GitHub;
using VisualArchitect.Api.Authentication.Presentation.Middleware;
using VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

namespace VisualArchitect.Api.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
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
            .AddCookie(AuthenticationConstants.Schemes.Cookie.Scheme, options => CookieOptionsBuilder.Build(options, configuration))
            .AddOAuth(AuthenticationConstants.Schemes.GitHub.Scheme, options => GitHubOAuthOptionsBuilder.Build(options, configuration));

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
