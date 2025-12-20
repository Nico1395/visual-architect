using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.ApplicationDesign;
using VisualArchitect.Api.Authentication;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;
using VisualArchitect.Api.Orchestration.Abstractions.Mediator;
using VisualArchitect.Api.Orchestration.Configuration;
using VisualArchitect.Api.Orchestration.Infrastructure.Context;

namespace VisualArchitect.Api.Orchestration;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectOrchestration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVisualArchitectAuthentication(configuration);
        services.AddVisualArchitectApplicationDesign();

        services.AddHttpContextAccessor();
        services.AddTransient<IMediator, Mediator.Mediator>();
        services.AddScoped<IClientUrlBuilder, ClientUrlBuilder>();

        services.AddOptions<AuthenticationOptions>()
            .Bind(configuration.GetSection("Authentication"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<ClientsOptions>()
            .Bind(configuration.GetSection("Clients"))
            .ValidateOnStart();
    
        services.AddTransient<IInterceptor, DomainInterfaceSaveChangesInterceptor>();
        services.AddDbContext<OrchestrationDbContext>();

        return services;
    }
}
