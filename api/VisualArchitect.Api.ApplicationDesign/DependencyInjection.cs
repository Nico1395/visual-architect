using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.ApplicationDesign.Application.Persistence;
using VisualArchitect.Api.ApplicationDesign.Domain.Repositories;
using VisualArchitect.Api.ApplicationDesign.Infrastructure.Persistence;

namespace VisualArchitect.Api.ApplicationDesign;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectApplicationDesign(this IServiceCollection services)
    {
        services.AddScoped<IApplicationDesignUnitOfWork, ApplicationDesignUnitOfWork>();
        services.AddScoped<IDesignProjectWriteRepository, DesignProjectWriteRepository>();

        return services;
    }
}
