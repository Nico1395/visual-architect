namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignProjectReadRepository
{
    Task<List<DesignProject>> GetForIdentityAsync(Guid identityId, bool includeDesignTasks, bool includeDesigns, CancellationToken cancellationToken);
}
