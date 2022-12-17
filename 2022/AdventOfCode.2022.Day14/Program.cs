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

        var spinner = new ConsoleAnimation
        {
            Delay = 300
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
        readonly List<string[,]> _sequence = new List<string[,]>();

        public int Delay { get; set; } = 200;
        public int PrintedLinesHeight = 0;

        int _counter;

        public ConsoleAnimation()
        {
            Console.CursorVisible = false;

            _sequence.Add(new string?[,]
            {
                { null, null, null, null, null, null, "+", null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, "#", null, null, null, "#", "#" },
                { null, null, null, null, "#", null, null, null, "#", null }
            }!);
            _sequence.Add(new string?[,]
            {
                { null, null, null, null, null, null, "+", null, null, null },
                { null, null, null, null, null, null, "o", null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, "#", null, null, null, "#", "#" },
                { null, null, null, null, "#", null, null, null, "#", null }
            }!);
            _sequence.Add(new string?[,]
            {
                { null, null, null, null, null, null, "+", null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, "o", null, null, null },
                { null, null, null, null, "#", null, null, null, "#", "#" },
                { null, null, null, null, "#", null, null, null, "#", null }
            }!);
            _sequence.Add(new string?[,]
            {
                { null, null, null, null, null, null, "+", null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, "#", null, "o", null, "#", "#" },
                { null, null, null, null, "#", null, null, null, "#", null }
            }!);
            _sequence.Add(new string?[,]
            {
                { null, null, null, null, null, null, "+", null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, "#", null, null, null, "#", "#" },
                { null, null, null, null, "#", null, "o", null, "#", null }
            }!);

            PrintedLinesHeight = _sequence[0].GetLength(0) + 2;
        }

        public void Turn()
        {
            _counter++;

            Thread.Sleep(Delay);

            var step = _counter % _sequence.Count;

            Console.WriteLine("Turn {0}, snowball {1}", step, 1);
            Console.WriteLine("");

            for (var y = 0; y < _sequence[0].GetLength(0); y++)
            {
                for (var x = 0; x < _sequence[0].GetLength(1); x++)
                {
                    if (string.IsNullOrEmpty(_sequence[step][y, x]))
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write(_sequence[step][y, x]);
                    }
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(0, Console.CursorTop - PrintedLinesHeight);
        }
    }
}