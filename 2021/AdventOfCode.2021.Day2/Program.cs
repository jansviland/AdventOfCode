﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AdventOfCode._2021.Day2;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices(((context, collection) => { collection.AddTransient<ISolutionService, SolutionService>(); }))
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

        var result = svc.Run(input);

        Log.Logger.Information("result: {Result}", result);
    }

    private static void BuildConfiguration(IConfigurationBuilder builder)
    {
        builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();
    }
}