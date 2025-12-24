using VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Commands;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Preferences.Application.Persistence;
using VisualArchitect.Api.Preferences.Domain;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Application.UseCases;

public static class SetPreference
{
    public sealed record SetPreferenceCommand(Guid IdentityId, string Key, string? Value, bool ResetToDefault) : ICommand;

    private sealed class SetPreferenceCommandHandler(
        IIdentitySettingReadRepository _identitySettingReadRepository,
        IIdentitySettingWriteRepository _identitySettingWriteRepository,
        ISettingReadRepository _settingReadRepository,
        IPreferencesUnitOfWork _preferencesUnitOfWork,
        IMediator _mediator) : ICommandHandler<SetPreferenceCommand>
    {
        public async Task<ICommandResponse> HandleAsync(SetPreferenceCommand request, CancellationToken cancellationToken)
        {
            // Does the identity even exist?
            var identityExistsResponse = await _mediator.SendAsync<IdentityExistsQuery, bool>(new IdentityExistsQuery(request.IdentityId), cancellationToken);
            if (identityExistsResponse.ResultedInFalse())
                return CommandResponseFactory.BadRequest_400().Build();

            // Does the setting even exists?
            var setting = await _settingReadRepository.GetByKeyAsync(request.Key, cancellationToken);
            if (setting == null)
                return CommandResponseFactory.BadRequest_400().Build();

            var identitySetting = await _identitySettingReadRepository.GetByKeyAsync(request.IdentityId, request.Key, cancellationToken);
            if (identitySetting == null)
            {
                // If its not set and we want to reset to default, we dont really have to do anything.
                if (request.ResetToDefault)
                    return CommandResponseFactory.Accepted_202().Build();

                await _identitySettingWriteRepository.AddAsync(new IdentitySetting()
                {
                    IdentityId = request.IdentityId,
                    SettingId = setting.Id,
                    Setting = setting,
                    Value = request.Value,
                }, cancellationToken);
            }
            else
            {
                if (request.ResetToDefault)
                {
                    // Delete the identity preference to fallback to the default value on the next query
                    await _identitySettingWriteRepository.DeleteAsync(identitySetting, cancellationToken);
                }
                else
                {
                    identitySetting.Value = request.Value;
                    await _identitySettingWriteRepository.UpdateAsync(identitySetting, cancellationToken);
                }
            }
            
            await _preferencesUnitOfWork.CommitAsync(cancellationToken);
            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}
