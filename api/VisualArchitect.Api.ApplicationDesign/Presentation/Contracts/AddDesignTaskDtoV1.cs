namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts;

public sealed class AddDesignTaskDtoV1
{
    public required string Name { get; init; }
    public required string DescriptionPayload { get; init; }
}
