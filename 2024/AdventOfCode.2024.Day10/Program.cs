using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

namespace AdventOfCode._2024.Day10;

internal static class Program
{
    private static void Main(string[] args)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var builder = new ConfigurationBuilder();

        // Log.Logger = new LoggerConfiguration()
        //     .ReadFrom.Configuration(BuildConfiguration(builder))
        //     .Enrich.FromLogContext()
        //     .CreateLogger();
        
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(((_, collection) => { collection.AddTransient<ISolutionService, SolutionService>(); }))
            // .UseSerilog()
            .Build();

        // Log.Logger.Information("args: {AllArguments}", string.Join(", ", args));
        AnsiConsole.MarkupLine("[bold green]Arguments:[/] {0}", string.Join(", ", args));

        var svc = ActivatorUtilities.CreateInstance<SolutionService>(host.Services);

        string[] input;
        if (args.Length == 0)
        {
            input = File.ReadAllLines("Assets/input.txt");
        }
        else
        {
            input = File.ReadAllLines(args[0]);
        }
        
        // Run Part 1
        var resultPart1 = svc.RunPart1(input);

        // Log Part 1 result
        AnsiConsole.MarkupLine("[bold yellow]------------------------------------[/]");
        AnsiConsole.MarkupLine("[bold green]Part 1 Result:[/] {0}", resultPart1);
        AnsiConsole.MarkupLine("[bold yellow]------------------------------------[/]");

        // Run Part 2
        var resultPart2 = svc.RunPart2(input);

        // Log Part 2 result
        AnsiConsole.MarkupLine("[bold yellow]------------------------------------[/]");
        AnsiConsole.MarkupLine("[bold green]Part 2 Result:[/] {0}", resultPart2);
        AnsiConsole.MarkupLine("[bold yellow]------------------------------------[/]");

        // Log elapsed time
        stopWatch.Stop();
        AnsiConsole.MarkupLine("[bold green]Elapsed time:[/] {0} ms", stopWatch.ElapsedMilliseconds);
    }

    private static IConfiguration BuildConfiguration(IConfigurationBuilder builder)
    {
        builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return configuration;
    }
}