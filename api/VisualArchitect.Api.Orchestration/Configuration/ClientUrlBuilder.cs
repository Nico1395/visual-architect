using System.Web;
using Microsoft.Extensions.Options;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;

namespace VisualArchitect.Api.Orchestration.Configuration;

public sealed class ClientUrlBuilder(IOptions<ClientsOptions> _clients) : IClientUrlBuilder
{
    public string? CreateUrl(string uri, IDictionary<string, object>? queryParameters = null)
    {
        var baseUrl = _clients.Value.VisualArchitect.BaseUrl;
        if (baseUrl == null)
            return null;

        var combinedPath = $"{baseUrl.ToString().TrimEnd('/')}/{uri.TrimStart('/')}";
        if (queryParameters == null || queryParameters.Count == 0)
            return combinedPath;

        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var kvp in queryParameters)
        {
            if (kvp.Value != null)
                query[kvp.Key] = kvp.Value.ToString();
        }

        var queryString = query.ToString();
        return string.IsNullOrWhiteSpace(queryString) ? combinedPath : $"{combinedPath}?{queryString}";
    }
}
