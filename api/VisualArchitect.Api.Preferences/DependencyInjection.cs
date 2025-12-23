using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Preferences.Application.Persistence;
using VisualArchitect.Api.Preferences.Domain.Repositories;
using VisualArchitect.Api.Preferences.Infrastructure.Persistence;

namespace VisualArchitect.Api.Preferences;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectPreferences(this IServiceCollection services)
    {
        services.AddScoped<IPreferencesUnitOfWork, PreferencesUnitOfWork>();
        services.AddScoped<IIdentitySettingReadRepository, IdentitySettingReadRepository>();
        services.AddScoped<IIdentitySettingWriteRepository, IdentitySettingWriteRepository>();
        services.AddScoped<ISettingReadRepository, SettingReadRepository>();

        return services;
    }
}
