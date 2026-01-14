namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignTaskReadRepository
{
    Task<long> GetGreatestNumberForProjectAsync(Guid projectId, CancellationToken cancellationToken);
}
