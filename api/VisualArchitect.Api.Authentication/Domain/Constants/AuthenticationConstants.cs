using Microsoft.AspNetCore.Authentication.Cookies;

namespace VisualArchitect.Api.Authentication.Domain.Constants;

public static class AuthenticationConstants
{
    public static class Schemes
    {
        public static string ToScheme(string providerKey)
        {
            ArgumentNullException.ThrowIfNull(providerKey, nameof(providerKey));
            return providerKey switch
            {
                GitHub.ProviderKey => GitHub.Scheme,
                Google.ProviderKey => Google.Scheme,
                Microsoft.ProviderKey => Microsoft.Scheme,
                _ => throw new ArgumentException("Unsupported provider key!", nameof(providerKey)),
            };
        }

        public static class Cookie
        {
            public const string Scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            public const string Name = "vac";
        }

        public static class Antiforgery
        {
            public const string HeaderName = "X-CSRF-TOKEN";
            public const string Name = "vac_csrf";
        }

        public static class GitHub
        {
            public const string Scheme = "GitHub";
            public const string ProviderKey = "github";

            public static class Configuration
            {
            }

            public static class ClaimTypes
            {
                public const string Id = "id";
                public const string Login = "login";
                public const string Email = "email";
            }
        }

        public static class Google
        {
            public const string Scheme = "Google";
            public const string ProviderKey = "google";
        }

        public static class Microsoft
        {
            public const string Scheme = "Microsoft";
            public const string ProviderKey = "microsoft";
        }
    }

    public static class Cors
    {
        public static class VirtualArchitectClient
        {
            public const string PolicyName = "visual-architect-client";
        }
    }

    public static class Configuration
    {
        public const string ClientBaseUrl = "VA_CLIENT_BASE_URL";
    }
}
