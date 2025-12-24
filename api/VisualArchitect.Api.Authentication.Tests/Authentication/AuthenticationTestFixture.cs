using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VisualArchitect.Api.Orchestration.Abstractions.Application.Persistence;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration;
using VisualArchitect.Api.Orchestration.Abstractions.Configuration.Options;
using VisualArchitect.Api.Orchestration.Configuration;
using VisualArchitect.Api.Orchestration.Infrastructure.Persistence;
using VisualArchitect.Api.Orchestration.Mediator;
using VisualArchitect.Api.Tests.Lib.Fixtures;

namespace VisualArchitect.Api.Authentication.Tests.Authentication;

public sealed class AuthenticationTestFixture : TestFixture
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        var assemblies = YieldAssemblies().ToList();

        services.AddDbContext<DbContext, AuthenticationTestDbContext>();
        services.AddOptions<AuthenticationOptions>().ValidateDataAnnotations().ValidateOnStart();
        services.AddOptions<ClientsOptions>().ValidateOnStart();
        services.AddOptions<ConnectionStringsOptions>().ValidateDataAnnotations().ValidateOnStart();

        services.AddHttpContextAccessor();
        services.AddMediator(assemblies);
        services.AddScoped<IClientUrlBuilder, ClientUrlBuilder>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddVisualArchitectAuthenticationPersistence();
    }

    public static IEnumerable<Assembly> YieldAssemblies()
    {
        yield return Assembly.Load("VisualArchitect.Api.Orchestration");
        yield return Assembly.Load("VisualArchitect.Api.Authentication");
    }
}
