﻿using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AdventOfCode._2024.Day16;

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

        var resultPart1 = svc.RunPart1(input);

        Log.Logger.Information("------------------------------------");
        Log.Logger.Information("result: {Result}", resultPart1);
        Log.Logger.Information("------------------------------------");

        var resultPart2 = svc.RunPart2(input);

        Log.Logger.Information("------------------------------------");
        Log.Logger.Information("result: {Result}", resultPart2);
        Log.Logger.Information("------------------------------------");

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