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

        for (var i = 0; i < 5; i++)
        {
            var startTime = DateTime.Now;
            var endTime = startTime.AddSeconds(5);

            while (DateTime.Now < endTime)
            {
                spinner.Turn();
            }
        }
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

        int _counter;

        public ConsoleAnimation()
        {
            Console.CursorVisible = false;
            // Console.SetBufferSize(200, 200);

            _sequence.Add(new string[,]
            {
                { "X", " " },
                { " ", " " },
            });
            _sequence.Add(new string[,]
            {
                { " ", "X" },
                { " ", " " },
            });
            _sequence.Add(new string[,]
            {
                { " ", " " },
                { " ", "X" },
            });
            _sequence.Add(new string[,]
            {
                { " ", " " },
                { "X", " " },
            });
        }

        public void Turn()
        {
            _counter++;

            Thread.Sleep(Delay);

            var step = _counter % _sequence.Count();

            Console.WriteLine("Turn {0}", step);
            Console.WriteLine("");

            for (var x = 0; x < 2; x++)
            {
                for (var y = 0; y < 2; y++)
                {
                    Console.Write(_sequence[step][x, y]);
                }

                Console.WriteLine();
            }

            Console.SetCursorPosition(0, Console.CursorTop - 4);


            // Console.Write(fullMessage);

            // Console.SetCursorPosition(Console.CursorLeft - msglength, Console.CursorTop);
        }
    }
}