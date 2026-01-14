namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignProjectWriteRepository
{
    Task AddAsync(DesignProject project, CancellationToken cancellationToken);
    Task UpdateAsync(DesignProject project, CancellationToken cancellationToken);
    Task DeleteAsync(DesignProject project, CancellationToken cancellationToken);
}
