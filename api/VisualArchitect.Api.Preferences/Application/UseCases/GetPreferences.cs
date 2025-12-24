using VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs;
using VisualArchitect.Api.Orchestration.Abstractions.Cqrs.Queries;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Preferences.Domain.Repositories;

namespace VisualArchitect.Api.Preferences.Application.UseCases;

public static class GetPreferences
{
    private sealed class GetPreferencesQueryHandler(
        IIdentitySettingReadRepository _identitySettingReadRepository,
        ISettingReadRepository _settingReadRepository,
        IMediator _mediator) : IQueryHandler<GetPreferencesQuery, IReadOnlyList<GetPreferencesQuery.Prefence>>
    {
        public async Task<IQueryResponse<IReadOnlyList<GetPreferencesQuery.Prefence>>> HandleAsync(GetPreferencesQuery request, CancellationToken cancellationToken)
        {
            var keys = request.Keys ?? [];

            // Does the identity even exists?
            var identityExistsResponse = await _mediator.SendAsync<IdentityExistsQuery, bool>(new IdentityExistsQuery(request.IdentityId), cancellationToken);
            if (identityExistsResponse.ResultedInFalse())
                return QueryResponseFactory.BadRequest_400<IReadOnlyList<GetPreferencesQuery.Prefence>>().Build();

            // Get all settings and if none even exists, we wont have to search any further -> thats a faulty request.
            var settings = await _settingReadRepository.GetByKeysOrAllAsync(keys, cancellationToken);
            if (settings.Count == 0)
                return QueryResponseFactory.BadRequest_400<IReadOnlyList<GetPreferencesQuery.Prefence>>().Build();

            var identitySettings = await _identitySettingReadRepository.GetByKeysOrAllAsync(request.IdentityId, keys, cancellationToken);
            var preferences = new List<GetPreferencesQuery.Prefence>();

            // Basically:
            // 1. Matchmake identity and key
            // 2. Get the identity setting's value and if not set yet, get the default value from the setting
            // 3. If it has never set, UpdatedAt should be null
            foreach (var setting in settings)
            {
                var identitySetting = identitySettings.SingleOrDefault(i => i.SettingId == setting.Id);
                preferences.Add(new GetPreferencesQuery.Prefence(
                    request.IdentityId,
                    setting.Key,
                    identitySetting != null ? identitySetting.Value : setting.DefaultValue,
                    setting.DefaultValue,
                    identitySetting?.UpdatedAt
                ));
            }

            return QueryResponseFactory.OK_200(preferences as IReadOnlyList<GetPreferencesQuery.Prefence>).Build();
        }
    }
}
