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
        IIdentityPreferenceReadRepository _identityPreferenceReadRepository,
        IIdentityPreferenceWriteRepository _identityPreferenceWriteRepository,
        IPreferenceReadRepository _preferenceReadRepository,
        IPreferencesUnitOfWork _preferencesUnitOfWork,
        IMediator _mediator) : ICommandHandler<SetPreferenceCommand>
    {
        public async Task<ICommandResponse> HandleAsync(SetPreferenceCommand request, CancellationToken cancellationToken)
        {
            // Does the identity even exist?
            var identityExistsResponse = await _mediator.SendAsync<IdentityExistsQuery, bool>(new IdentityExistsQuery(request.IdentityId), cancellationToken);
            if (identityExistsResponse.ResultedInFalse())
                return CommandResponseFactory.BadRequest_400().Build();

            // Does the preference even exists?
            var preference = await _preferenceReadRepository.GetByKeyAsync(request.Key, cancellationToken);
            if (preference == null)
                return CommandResponseFactory.BadRequest_400().Build();

            var identityPreference = await _identityPreferenceReadRepository.GetByKeyAsync(request.IdentityId, request.Key, cancellationToken);
            if (identityPreference == null)
            {
                // If its not set and we want to reset to default, we dont really have to do anything.
                if (request.ResetToDefault)
                    return CommandResponseFactory.Accepted_202().Build();

                await _identityPreferenceWriteRepository.AddAsync(new IdentityPreference()
                {
                    IdentityId = request.IdentityId,
                    PreferenceId = preference.Id,
                    Preference = preference,
                    Value = request.Value,
                }, cancellationToken);
            }
            else
            {
                if (request.ResetToDefault)
                {
                    // Delete the identity preference to fallback to the default value on the next query
                    await _identityPreferenceWriteRepository.DeleteAsync(identityPreference, cancellationToken);
                }
                else
                {
                    identityPreference.Value = request.Value;
                    await _identityPreferenceWriteRepository.UpdateAsync(identityPreference, cancellationToken);
                }
            }
            
            await _preferencesUnitOfWork.CommitAsync(cancellationToken);
            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}
