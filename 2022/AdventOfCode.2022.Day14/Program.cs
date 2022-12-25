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
        //
        // stopWatch.Stop();
        // Log.Logger.Information("Elapsed time: {Elapsed} ms", stopWatch.ElapsedMilliseconds);

        var startGrid = solutionService.ParseInput(input);
        var frames = solutionService.CreateSequence(startGrid);

        var lastFrame = frames.Last();
        Log.Logger.Information("Total sand count: {SandCount}", lastFrame.SandCount);

        // BUG: only prints 95 lines not 162
        // PrintFrame(frames, frames.Count - 1);

        Console.CursorVisible = false;
        for (var i = 0; i < frames.Count; i++)
        {
            PrintFrame(frames, i);
            Thread.Sleep(200);
            Console.SetCursorPosition(0, Console.CursorTop - frames[0].Grid.GetLength(1) - 2);
        }

        Console.CursorVisible = true;

        // TODO: Print final frame
        // Log.Logger.Information("Final result:");
        //
        // var print = solutionService.CreatePrintableOutput(lastFrame);
        // foreach (var line in print)
        // {
        //     Log.Logger.Information(line.Join(""));
        // }

        // var spinner = new ConsoleAnimation(frames)
        // {
        //     Delay = 50
        // };

        // PrintFrame(frames);

        // BUG: make it run until the end
        // var startTime = DateTime.Now;
        // var endTime = startTime.AddMinutes(30);
        //
        // while (DateTime.Now < endTime)
        // {
        //     spinner.Turn();
        // }

        // set cursor position back again
        // Console.SetCursorPosition(0, startGrid.YMax + 1);
        // Console.SetCursorPosition(0, Console.CursorTop + frames[0].Grid.GetLength(1) - 2);
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

    private static void PrintFrame(List<Frame> frames, int frameIndex)
    {
        StringBuilder sb = new();

        Console.WriteLine("Turn: {0}, sand count: {1}, Sand position: ({2}, {3}), YMax: {4}    ",
            frameIndex, frames[frameIndex].SandCount, frames[frameIndex].SandX, frames[frameIndex].SandY, frames[frameIndex].YMax);

        Console.WriteLine("");

        for (var y = 0; y < frames[0].Grid.GetLength(1); y++)
        {
            for (var x = 0; x < frames[0].Grid.GetLength(0); x++)
            {
                if (string.IsNullOrEmpty(frames[frameIndex].Grid[x, y]))
                {
                    sb.Append(".");
                }
                else
                {
                    sb.Append(frames[frameIndex].Grid[x, y]);
                }

                if (x == frames[0].Grid.GetLength(0) - 1)
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