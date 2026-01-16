namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts.DesignTasks;

public sealed class UpdateDesignTaskDtoV1
{
    public string Name { get; set; } = string.Empty;
    public string DescriptionPayload { get; set; } = string.Empty;
    public int Status { get; set; } = -1;
}
