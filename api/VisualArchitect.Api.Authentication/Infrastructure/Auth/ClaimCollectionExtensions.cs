using System.Security.Claims;

namespace VisualArchitect.Api.Authentication.Infrastructure.Auth;

public static class ClaimCollectionExtensions
{
    public static IEnumerable<string> GetClaims(this IEnumerable<Claim> claims, string claimType)
    {
        return claims.Where(c => c.Type == claimType).Select(c => c.Value);
    }

    public static string? GetClaimOrDefault(this IEnumerable<Claim> claims, string claimType, string? @default = null)
    {
        return claims.GetClaims(claimType).FirstOrDefault() ?? @default;
    }

    public static string? GetClaimOrAlternative(this IEnumerable<Claim> claims, string claimType, string? alternative = null, string? @default = null)
    {
        return claims.GetClaims(claimType).FirstOrDefault()
            ?? (alternative != null ? claims.GetClaims(alternative).FirstOrDefault() : null)
            ?? @default;
    }

    public static string GetClaim(this IEnumerable<Claim> claims, string claimType)
    {
        return claims.GetClaims(claimType).First();
    }

    public static string? GetSingleClaimOrDefault(this IEnumerable<Claim> claims, string claimType, string? @default = null)
    {
        return claims.GetClaims(claimType).SingleOrDefault() ?? @default;
    }

    public static string? GetSingleClaimOrAlternative(this IEnumerable<Claim> claims, string claimType, string? alternative = null, string? @default = null)
    {
        return claims.GetClaims(claimType).SingleOrDefault()
            ?? (alternative != null ? claims.GetClaims(alternative).SingleOrDefault() : null)
            ?? @default;
    }

    public static string GetSingleClaim(this IEnumerable<Claim> claims, string claimType)
    {
        return claims.GetClaims(claimType).Single();
    }
}
