using Microsoft.Extensions.DependencyInjection;

namespace VisualArchitect.Api.Tests.Lib.Fixtures;

public abstract class TestFixture : ITestFixture
{
    private sealed class TestScope(IServiceScope _serviceScope) : ITestScope
    {
        public void Dispose()
        {
            _serviceScope.Dispose();
        }

        public object? GetService(Type serviceType)
        {
            return _serviceScope.ServiceProvider.GetService(serviceType);
        }
    }

    public TestFixture()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        InternalServiceProvider = services.BuildServiceProvider();
    }

    protected IServiceProvider InternalServiceProvider { get; }

    public ITestScope GetScope()
    {
        return new TestScope(InternalServiceProvider.CreateScope());
    }

    protected abstract void ConfigureServices(IServiceCollection services);
}
