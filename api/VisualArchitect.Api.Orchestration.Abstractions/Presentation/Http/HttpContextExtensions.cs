using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace VisualArchitect.Api.Orchestration.Abstractions.Presentation.Http;

public static class HttpContextExtensions
{
    public static bool TryGetIdentityId(this HttpContext httpContext, [NotNullWhen(true)] out Guid identityId)
    {
        identityId = default;

        var idClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(idClaim) || !Guid.TryParse(idClaim, out var id))
            return false;

        identityId = id;
        return true;
    }
}
