using System.Diagnostics;
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

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(((_, collection) => { collection.AddTransient<ISolutionService, SolutionService>(); }))
            .Build();

        AnsiConsole.MarkupLine("[bold green]Arguments:[/] {0}", string.Join(", ", args));

        var svc = ActivatorUtilities.CreateInstance<SolutionService>(host.Services);

        var input = File.ReadAllLines("Assets/input.txt");
        
        // Run Part 1
        var animate = args.Contains("animate");
        var resultPart1 = svc.RunPart1(input, animate);

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
}