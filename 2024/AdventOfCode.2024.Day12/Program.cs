using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

namespace AdventOfCode._2024.Day12;

internal static class Program
{
    private static void Main(string[] args)
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(((_, collection) => { collection.AddTransient<ISolutionService, SolutionService>(); }))
            .Build();

        var svc = ActivatorUtilities.CreateInstance<SolutionService>(host.Services);
        var input = File.ReadAllLines("Assets/input.txt");
        // var input = File.ReadAllLines("Assets/test-input.txt");
        // var input = File.ReadAllLines("Assets/test-input-large.txt");
        
        var animate = args.Contains("animate");
        var resultPart1 = svc.RunPart1(input, animate);
        AnsiConsole.MarkupLine("[bold yellow]------------------------------------[/]");
        AnsiConsole.MarkupLine("[bold green]Part 1 Result:[/] {0}", resultPart1);

        var resultPart2 = svc.RunPart2(input);
        AnsiConsole.MarkupLine("[bold green]Part 2 Result:[/] {0}", resultPart2);
        AnsiConsole.MarkupLine("[bold yellow]------------------------------------[/]");

        stopWatch.Stop();
        AnsiConsole.MarkupLine("[bold green]Elapsed time:[/] {0} ms", stopWatch.ElapsedMilliseconds);
    }
}