namespace VisualArchitect.Api.ApplicationDesign.Presentation.Contracts.Designs;

public sealed class AddDesignDtoV1
{
    public string Name { get; set; } = string.Empty;
    public string? DescriptionPayload { get; set; }
    public int Type { get; set; }
}
