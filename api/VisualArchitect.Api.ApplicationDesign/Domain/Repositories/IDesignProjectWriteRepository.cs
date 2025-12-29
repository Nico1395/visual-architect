namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignProjectWriteRepository
{
    Task AddAsync(DesignProject project, CancellationToken cancellationToken);
}
