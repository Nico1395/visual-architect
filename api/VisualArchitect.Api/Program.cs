namespace VisualArchitect.Api;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddVisualArchitect();

        var app = builder.Build();

        app.Run();
    }
}
