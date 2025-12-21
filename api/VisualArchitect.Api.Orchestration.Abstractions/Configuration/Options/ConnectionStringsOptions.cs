namespace VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;

public sealed class ConnectionStringsOptions
{
    public ConnectionStringsOptions() { }

    public ConnectionStringsOptions(string @default)
    {
        Default = @default;
    }

    public string? Default { get; set; }
}
