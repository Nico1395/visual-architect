using Microsoft.Extensions.Configuration;
using VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

namespace VisualArchitect.Api.Orchestration.Abstractions.Configuration;

public static class ConfigurationExtensions
{
    public static string GetRequired(this IConfiguration configuration, string key)
    {
        return configuration[key] ?? throw new MissingConfigurationException(key);
    }
}
