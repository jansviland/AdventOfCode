namespace AdventOfCode._2023.Day5;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public int RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<SolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 5 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // TODO: instead of a dictionary, we could use long[] and just add the offset: 
        
        // so seedToSoil [0, 2, 4, 6, 8] would map [1, 1, 1, 1, 1] to [1, 3, 5, 7, 9]
        
        var seedToSoilDictionary = new Dictionary<long, long>();
        var soilToFertilizerDictionary = new Dictionary<long, long>();
        var fertilizerToWaterDictionary = new Dictionary<long, long>();
        var waterToLightDictionary = new Dictionary<long, long>();
        var lightToTemperatureDictionary = new Dictionary<long, long>();
        var tempratureToHumidityDictionary = new Dictionary<long, long>();
        var humidityToLocationDictionary = new Dictionary<long, long>();

        // parse input
        long[] seeds = input[0].Split(':').Last().Trim().Split(' ').Select(long.Parse).ToArray();

        // 0 : Seed to soil
        // 1 : Soil to fertilizer
        // 2 : Fertilizer to water
        var currentMap = 0;

        for (var i = 3; i < input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(input[i]))
            {
                continue;
            }

            string[] split = input[i].Split(' ');
            if (!long.TryParse(split[0], out _))
            {
                currentMap++;
            }
            else
            {
                long[] values = split.Select(long.Parse).ToArray();

                long destinationRangeStart = values[0];
                long sourceRangeStart = values[1];
                long rangeLength = values[2];

                // seed to soil
                if (currentMap == 0)
                {
                    for (var j = 0; j < rangeLength; j++)
                    {
                        seedToSoilDictionary.Add(sourceRangeStart + j, destinationRangeStart + j);
                    }
                }
                else if (currentMap == 1)
                {
                    for (var j = 0; j < rangeLength; j++)
                    {
                        soilToFertilizerDictionary.Add(sourceRangeStart + j, destinationRangeStart + j);
                    }
                }
                else if (currentMap == 2)
                {
                    for (var j = 0; j < rangeLength; j++)
                    {
                        fertilizerToWaterDictionary.Add(sourceRangeStart + j, destinationRangeStart + j);
                    }
                }
                else if (currentMap == 3)
                {
                    for (var j = 0; j < rangeLength; j++)
                    {
                        waterToLightDictionary.Add(sourceRangeStart + j, destinationRangeStart + j);
                    }
                }
                else if (currentMap == 4)
                {
                    for (var j = 0; j < rangeLength; j++)
                    {
                        lightToTemperatureDictionary.Add(sourceRangeStart + j, destinationRangeStart + j);
                    }
                }
                else if (currentMap == 5)
                {
                    for (var j = 0; j < rangeLength; j++)
                    {
                        tempratureToHumidityDictionary.Add(sourceRangeStart + j, destinationRangeStart + j);
                    }
                }
                else if (currentMap == 6)
                {
                    for (var j = 0; j < rangeLength; j++)
                    {
                        humidityToLocationDictionary.Add(sourceRangeStart + j, destinationRangeStart + j);
                    }
                }
            }
        }

        var lowestLocation = long.MaxValue;
        for (var i = 0; i < seeds.Length; i++)
        {
            // If it does note exist, it´s one to one

            long seed = seeds[i];
            long soil = seedToSoilDictionary.TryGetValue(seed, out long value) ? value : seed;
            long fertilizer = soilToFertilizerDictionary.TryGetValue(soil, out value) ? value : soil;
            long water = fertilizerToWaterDictionary.TryGetValue(fertilizer, out value) ? value : fertilizer;
            long light = waterToLightDictionary.TryGetValue(water, out value) ? value : water;
            long temperature = lightToTemperatureDictionary.TryGetValue(light, out value) ? value : light;
            long humidity = tempratureToHumidityDictionary.TryGetValue(temperature, out value) ? value : temperature;
            long location = humidityToLocationDictionary.TryGetValue(humidity, out value) ? value : humidity;

            _logger.LogInformation("Seed {Seed} -> Soil {Soil} -> Fertilizer {Fertilizer} -> Water {Water} -> Light {Light} -> Temperature {Temperature} -> Humidity {Humidity} -> Light {Light2}", seed, soil, fertilizer, water, light, temperature, humidity, location);

            lowestLocation = Math.Min(lowestLocation, location);
        }


        return lowestLocation;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 5 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}