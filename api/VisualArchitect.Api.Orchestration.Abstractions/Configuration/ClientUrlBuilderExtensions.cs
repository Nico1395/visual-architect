using VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

namespace VisualArchitect.Api.Orchestration.Abstractions.Configuration;

public static class ClientUrlBuilderExtensions
{
    public static string CreateRequiredBaseUrl(this IClientUrlBuilder builder, IDictionary<string, object>? queryParameters = null)
    {
        return builder.CreateUrl(string.Empty, queryParameters) ?? throw new MissingConfigurationException("The client base URL has not been configured.");
    }

    public static string? CreateUrl(this IClientUrlBuilder builder, string uri, IEnumerable<(string key, object value)> queryParameters)
    {
        return builder.CreateUrl(uri, queryParameters.ToDictionary());
    }

    public static string CreateRequiredUrl(this IClientUrlBuilder builder, string uri, IDictionary<string, object>? queryParameters = null)
    {
        return builder.CreateUrl(uri, queryParameters) ?? throw new MissingConfigurationException("The client base URL has not been configured.");
    }

    public static string CreateRequiredUrl(this IClientUrlBuilder builder, string uri, IEnumerable<(string key, object value)> queryParameters)
    {
        return builder.CreateRequiredUrl(uri, queryParameters.ToDictionary());
    }
}
