using VisualArchitect.Api.Orchestration.Abstractions.Application.Persistence;
using VisualArchitect.Api.Preferences.Application.Persistence;

namespace VisualArchitect.Api.Preferences.Infrastructure.Persistence;

public sealed class PreferencesUnitOfWork(IUnitOfWork _unitOfWork) : IPreferencesUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.CommitAsync(cancellationToken);
    }
}
