namespace VisualArchitect.Api.ApplicationDesign.Domain.Repositories;

public interface IDesignWriteRepository
{
    Task AddAsync(Design design, CancellationToken cancellationToken);
}
