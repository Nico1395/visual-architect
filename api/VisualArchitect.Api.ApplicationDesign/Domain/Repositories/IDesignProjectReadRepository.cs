namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignProjectReadRepository
{
    Task<DesignProject?> GetForIdentityByIdAsync(Guid identityId, Guid projectId, bool includeDesignTasks, bool includeDesigns, CancellationToken cancellationToken);
    Task<List<DesignProject>> GetAllForIdentityAsync(Guid identityId, bool includeDesignTasks, bool includeDesigns, CancellationToken cancellationToken);
}
