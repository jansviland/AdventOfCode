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

        // var result = svc.RunPart1(input);
        // Log.Logger.Information("result: {Result}", result);
        //
        // var resultPart2 = svc.RunPart2(input);
        // Log.Logger.Information("result: {Result}", resultPart2);
        //
        // stopWatch.Stop();
        // Log.Logger.Information("Elapsed time: {Elapsed} ms", stopWatch.ElapsedMilliseconds);

        var spinner = new ConsoleAnimation(svc, input)
        {
            Delay = 500
        };

        for (var i = 0; i < 10; i++)
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddSeconds(5);

            while (DateTime.Now < endTime)
            {
                spinner.Turn();
            }
        }

        // set fontsize to 8

        // set cursor position back again
        Console.SetCursorPosition(0, Console.CursorTop + spinner.PrintedLinesHeight);

        Log.Logger.Information("Done");
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

    private class ConsoleAnimation
    {
        // private readonly ISolutionService _solutionService;
        readonly List<Frame> _frames = new List<Frame>();

        public int Delay { get; set; } = 200;
        public int PrintedLinesHeight = 0;

        int _counter;

        public ConsoleAnimation(ISolutionService solutionService, string[] input)
        {
            var startGrid = solutionService.ParseInput(input);
            _frames = solutionService.CreateSequence(startGrid);

            Console.CursorVisible = false;
            // Console.SetBufferSize(Console.BufferWidth, 32766);

            PrintedLinesHeight = _frames[0].Grid.GetLength(1) + 2;
        }

        public void Turn()
        {
            _counter++;

            Thread.Sleep(Delay);

            var step = _counter % _frames.Count;

            Console.WriteLine("Turn {0}, snowball {1}", step, 1);
            Console.WriteLine("");

            var sb = new StringBuilder();

            for (var y = 0; y < _frames[0].Grid.GetLength(1); y++)
            {
                sb.Clear();
                for (var x = 0; x < _frames[0].Grid.GetLength(0); x++)
                {
                    if (string.IsNullOrEmpty(_frames[step].Grid[x, y]))
                    {
                        sb.Append(".");
                        // Console.Write(".");
                    }
                    else
                    {
                        sb.Append(_frames[step].Grid[x, y]);
                        // Console.Write(_frames[step].Grid[x, y]);
                    }

                }

                Console.WriteLine(sb.ToString());
                // Console.WriteLine();
            }

            Console.SetCursorPosition(0, Console.CursorTop - PrintedLinesHeight);
        }
    }
}