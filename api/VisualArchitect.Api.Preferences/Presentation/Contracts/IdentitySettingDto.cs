using VisualArchitect.Api.Orchestration.Abstractions.Application.UseCases;

namespace VisualArchitect.Api.Preferences.Presentation.Contracts;

internal class IdentityPreferenceDtoV1
{
    public required Guid IdentityId { get; init; }
    public required string Key { get; init; }
    public required string? Value { get; init; }
    public required string? DefaultValue { get; init; }
    public required DateTime? UpdatedAt { get; init; }

    public static IdentityPreferenceDtoV1 From(GetPreferencesQuery.Prefence preference)
    {
        return new()
        {
            IdentityId = preference.IdentityId,
            Key = preference.Key,
            Value = preference.Value,
            DefaultValue = preference.DefaultValue,
            UpdatedAt = preference.UpdatedAt,
        };
    }
}
