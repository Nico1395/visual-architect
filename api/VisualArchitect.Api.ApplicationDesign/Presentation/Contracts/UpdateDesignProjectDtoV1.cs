namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;

public sealed class UpdateDesignProjectDtoV1
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? DescriptionPayload { get; init; }
}
