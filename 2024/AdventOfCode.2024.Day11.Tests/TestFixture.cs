using AdventOfCode._2024.Day11;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdventOfCodeAdventOfCode._2024.Day11.Tests;

public class TestFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        => services
            .AddTransient<ISolutionService, SolutionService>();

    protected override ValueTask DisposeAsyncCore()
        => new();

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        yield return new TestAppSettings { Filename = "appsettings.json", IsOptional = false };
    }
}