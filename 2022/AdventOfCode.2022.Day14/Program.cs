using System.Diagnostics;
using System.Net.Http.Headers;
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

    private static void PrintFrames(List<Frame> frames, int frameIndex)
    {
        StringBuilder sb = new();

        Console.WriteLine("Turn: {0}, sand count: {1}, Sand position: ({2}, {3}), YMax: {4}    ",
            frameIndex, frames[frameIndex].SandCount, frames[frameIndex].SandX, frames[frameIndex].SandY, frames[frameIndex].YMax);

        Console.WriteLine("");

        for (var y = 0; y < frames[frameIndex].Grid.GetLength(1); y++)
        {
            for (var x = 0; x < frames[frameIndex].Grid.GetLength(0); x++)
            {
                if (string.IsNullOrEmpty(frames[frameIndex].Grid[x, y]))
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append(frames[frameIndex].Grid[x, y]);
                }

                if (x == frames[frameIndex].Grid.GetLength(0) - 1)
                {
                    sb.Append("   " + y);
                }
            }

            // add new line
            sb.Append(Environment.NewLine);
        }

        Console.Write(sb.ToString());
        sb.Clear();
    }
}