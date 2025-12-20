using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace VisualArchitect.Api.Authentication.Infrastructure.Auth.GitHub;

public static class GitHubOAuthEventHandler
{
    public static async Task HandleOnCreatingTicketAsync(OAuthCreatingTicketContext context)
    {
        // Fetch easily accessible user info
        var userRequest = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
        userRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
        userRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var userResponse = await context.Backchannel.SendAsync(userRequest, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
        userResponse.EnsureSuccessStatusCode();

        using var userJson = JsonDocument.Parse(await userResponse.Content.ReadAsStringAsync());
        var root = userJson.RootElement;

        context.RunClaimActions(root);

        if (context.Identity == null)
            return;

        // Access GitHubs private emails via /user/emails
        string? email = null;
        if (root.TryGetProperty("email", out var emailProp) && emailProp.ValueKind == JsonValueKind.String)
            email = emailProp.GetString();

        if (string.IsNullOrWhiteSpace(email))
        {
            var emailRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user/emails");
            emailRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
            emailRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));

            var emailResponse = await context.Backchannel.SendAsync(emailRequest, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
            emailResponse.EnsureSuccessStatusCode();

            using var emailJson = JsonDocument.Parse(await emailResponse.Content.ReadAsStringAsync());
            var primaryEmail = emailJson.RootElement
                .EnumerateArray()
                .FirstOrDefault(e => e
                    .GetProperty("primary")
                    .GetBoolean())
                .GetProperty("email")
                    .GetString();

            email = primaryEmail;
        }

        if (!string.IsNullOrWhiteSpace(email))
            context.Identity.AddClaim(new Claim(ClaimTypes.Email, email));
    }
}
