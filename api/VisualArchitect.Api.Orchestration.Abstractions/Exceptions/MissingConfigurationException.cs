namespace VisualArchitect.Api.Orchestration.Abstractions.Exceptions;

public sealed class MissingConfigurationException(string configurationName) : Exception($"Configuration with key {configurationName} has to be configured!")
{
}
