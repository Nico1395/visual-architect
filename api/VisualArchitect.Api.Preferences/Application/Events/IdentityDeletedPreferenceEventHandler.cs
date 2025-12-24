using VisualArchitect.Api.Orchestration.Abstractions.Application.Events;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Preferences.Application.Persistence;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Application.Events;

internal sealed class IdentityDeletedPreferenceEventHandler(
    IIdentitySettingReadRepository _identitySettingReadRepository,
    IIdentitySettingWriteRepository _identitySettingWriteRepository,
    IPreferencesUnitOfWork _preferencesUnitOfWork) : INotificationHandler<IdentityDeletedEvent>
{
    public async Task HandleAsync(IdentityDeletedEvent notification, CancellationToken cancellationToken)
    {
        var preferences = await _identitySettingReadRepository.GetByKeysOrAllAsync(notification.IdentityId, [], cancellationToken);

        await _identitySettingWriteRepository.DeleteRangeAsync(preferences, cancellationToken);
        await _preferencesUnitOfWork.CommitAsync(cancellationToken);
    }
}
