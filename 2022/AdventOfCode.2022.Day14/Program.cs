using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AdventOfCode._2022.Day14;

internal static class Program
{
    private static void Main(string[] args)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var builder = new ConfigurationBuilder();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(BuildConfiguration(builder))
            .Enrich.FromLogContext()
            .CreateLogger();

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(((_, collection) => { collection.AddTransient<ISolutionService, SolutionService>(); }))
            .UseSerilog()
            .Build();

        Log.Logger.Information("args: {AllArguments}", string.Join(", ", args));

        Log.Logger.Verbose("verbose");
        Log.Logger.Information("info");
        Log.Logger.Warning("warn");
        Log.Logger.Error("error");
        Log.Logger.Fatal("fatal");
        Log.Logger.Debug("debug");

        var solutionService = ActivatorUtilities.CreateInstance<SolutionService>(host.Services);

        string[] input;
        if (args.Length == 0)
        {
            input = File.ReadAllLines("Assets/input.txt");
            // input = File.ReadAllLines("Assets/test-input.txt");
        }
        else
        {
            input = File.ReadAllLines(args[0]);
        }

        // var result = solutionService.RunPart1(input);
        // Log.Logger.Information("result: {Result}", result);
        //
        // var resultPart2 = solutionService.RunPart2(input);
        // Log.Logger.Information("result: {Result}", resultPart2);

        // part 1
        Log.Logger.Information("PART 1");

        Console.CursorVisible = false;

        var startGrid = solutionService.ParseInput(input);
        var frames = solutionService.CreateSequence(startGrid, false);

        var lastFrame = frames.Last();
        solutionService.PrintFrame(lastFrame);

        Console.SetCursorPosition(0, Console.CursorTop + lastFrame.Grid.GetLength(1) + 2);

        // part 2
        Log.Logger.Information("PART 2");

        startGrid = solutionService.ParseInputPart2(input);
        frames = solutionService.CreateSequencePart2(startGrid, 3);

        lastFrame = frames.Last();

        solutionService.PrintFrame(lastFrame);

        Console.SetCursorPosition(0, Console.CursorTop + lastFrame.Grid.GetLength(1) + 2);

        stopWatch.Stop();
        Log.Logger.Information("Elapsed time: {Elapsed} ms", stopWatch.ElapsedMilliseconds);
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