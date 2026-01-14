namespace VisualArchitect.Api.Orchestration.Mediator.Middleware;

internal sealed record CqrsRequestValidationResult(IReadOnlyDictionary<string, string[]> Errors)
{
    public bool IsValid => Errors.Count == 0;
}
