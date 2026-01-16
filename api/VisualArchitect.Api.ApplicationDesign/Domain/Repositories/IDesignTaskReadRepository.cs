namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignTaskReadRepository
{
    Task<DesignTask?> GetByIdAsync(Guid taskId, bool includeDesigns, CancellationToken cancellationToken);
    Task<DesignTask?> GetByProjectIdAndNumberAsync(Guid projectId, long taskNumber, bool includeDesigns, CancellationToken cancellationToken);
    Task<long> GetGreatestNumberForProjectAsync(Guid projectId, CancellationToken cancellationToken);
}
