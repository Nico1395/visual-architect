namespace VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;

public sealed class ClientsOptions
{
    public VisualArchitectClientOptions VisualArchitect { get; init; } = new();
}
