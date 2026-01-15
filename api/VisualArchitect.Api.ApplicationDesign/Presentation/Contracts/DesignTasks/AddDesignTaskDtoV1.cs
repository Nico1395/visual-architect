namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts.DesignTasks;

public sealed class AddDesignTaskDtoV1
{
    public string Name { get; init; } = string.Empty;
    public string DescriptionPayload { get; init; } = string.Empty;
}
