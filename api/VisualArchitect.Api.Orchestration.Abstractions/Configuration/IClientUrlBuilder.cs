namespace VisualArchitect.Api.Orchestration.Abstractions.Configuration;

public interface IClientUrlBuilder
{
    string? CreateUrl(string uri, IDictionary<string, object>? queryParameters = null);
}
