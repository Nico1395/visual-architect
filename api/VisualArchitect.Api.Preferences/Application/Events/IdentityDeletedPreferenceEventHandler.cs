using VisualArchitect.Api.Orchestration.Abstractions.Application.Events;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Preferences.Application.Persistence;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Application.Events;

internal sealed class IdentityDeletedPreferenceEventHandler(
    IIdentityPreferenceReadRepository _identityPreferenceReadRepository,
    IIdentityPreferenceWriteRepository _identityPreferenceWriteRepository,
    IPreferencesUnitOfWork _preferencesUnitOfWork) : INotificationHandler<IdentityDeletedEvent>
{
    public async Task HandleAsync(IdentityDeletedEvent notification, CancellationToken cancellationToken)
    {
        var preferences = await _identityPreferenceReadRepository.GetByKeysOrAllAsync(notification.IdentityId, [], cancellationToken);

        await _identityPreferenceWriteRepository.DeleteRangeAsync(preferences, cancellationToken);
        await _preferencesUnitOfWork.CommitAsync(cancellationToken);
    }
}
