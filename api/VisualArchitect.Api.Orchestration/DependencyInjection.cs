using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.ApplicationDesign;
using VisualArchitect.Api.Authentication;
using VisualArchitect.Api.Orchestration.Abstractions.Application.Persistence;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;
using VisualArchitect.Api.Orchestration.Abstractions.Infrastructure.Transactions;
using VisualArchitect.Api.Orchestration.Configuration;
using VisualArchitect.Api.Orchestration.Infrastructure.Context;
using VisualArchitect.Api.Orchestration.Infrastructure.Persistence;
using VisualArchitect.Api.Orchestration.Infrastructure.Transactions;
using VisualArchitect.Api.Orchestration.Mediator;
using VisualArchitect.Api.Preferences;

namespace VisualArchitect.Api.Orchestration;

public static class DependencyInjection
{
    public static IServiceCollection AddVisualArchitectOrchestration(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = YieldAssemblies().ToList();

        services.AddVisualArchitectAuthentication(configuration);
        services.AddVisualArchitectApplicationDesign();
        services.AddVisualArchitectPreferences();

        services.AddHttpContextAccessor();
        services.AddMediator(assemblies);
        services.AddScoped<IClientUrlBuilder, ClientUrlBuilder>();

        services.AddOptions<AuthenticationOptions>().Bind(configuration.GetSection("Authentication")).ValidateDataAnnotations().ValidateOnStart();
        services.AddOptions<ClientsOptions>() .Bind(configuration.GetSection("Clients")).ValidateOnStart();
        services.AddOptions<ConnectionStringsOptions>().Bind(configuration.GetSection("ConnectionStrings")).ValidateDataAnnotations().ValidateOnStart();

        services.AddTransient<IInterceptor, DomainInterfaceSaveChangesInterceptor>();
        services.AddSingleton(new DbContextConfiguration() { AssembliesToScan = assemblies });
        services.AddDbContext<OrchestrationDbContext>();
        services.AddScoped<DbContext>(sp => sp.GetRequiredService<OrchestrationDbContext>());               // For as long as we only use one DbContext, which will probably be sufficient for a long time!

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ITransactionFactory, TransactionFactory>();

        return services;
    }

    public static IEnumerable<Assembly> YieldAssemblies()
    {
        yield return Assembly.Load("VisualArchitect.Api.ApplicationDesign");
        yield return Assembly.Load("VisualArchitect.Api.Authentication");
        yield return Assembly.Load("VisualArchitect.Api.Preferences");
        yield return Assembly.Load("VisualArchitect.Api.Orchestration");
    }
}
