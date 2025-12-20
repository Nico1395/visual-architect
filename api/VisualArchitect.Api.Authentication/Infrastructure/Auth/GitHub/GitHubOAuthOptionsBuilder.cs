using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VisualArchitect.Api.Authentication.Domain.Constants;
using VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

namespace VisualArchitect.Api.Authentication.Infrastructure.Auth.GitHub;

public static class GitHubOAuthOptionsBuilder
{
    public static void Build(OAuthOptions options, IConfiguration configuration)
    {
        var gitHubClientId = configuration[AuthenticationConstants.Schemes.GitHub.Configuration.ClientId] ?? throw new MissingConfigurationException(AuthenticationConstants.Schemes.GitHub.Configuration.ClientId);
        var gitHubClientSecret = configuration[AuthenticationConstants.Schemes.GitHub.Configuration.ClientSecret] ?? throw new MissingConfigurationException(AuthenticationConstants.Schemes.GitHub.Configuration.ClientSecret);

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
            OnCreatingTicket = GitHubOAuthEventHandler.HandleOnCreatingTicketAsync,
        };
    }
}
