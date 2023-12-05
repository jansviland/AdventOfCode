namespace AdventOfCode._2023.Day5;

public class DistanceMap
{
    public int Type { get; set; }
    public List<DistanceMapElement> Elements { get; set; }
}

public class DistanceMapElement
{
    public int Type { get; set; }

    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }
}

public class SolutionServiceV2 : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionServiceV2(ILogger<SolutionServiceV2> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 5 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);


        // throw new NotImplementedException();

        // TODO: instead of a dictionary, we could use long[] and just add the offset: 

        // so seedToSoil [0, 2, 4, 6, 8] would map [1, 1, 1, 1, 1] to [1, 3, 5, 7, 9]

        // TODO: find the lowest source range start, this is index 0 of the array, 
        // TODO: find the highest source range start + range length, this is the index of the last element in the array
        // TODO: then fill the array with 0 values from 0 index to the end of the array
        // TODO: then fill the array with values, 0 if there is no mapping, offset if there is a source -> destination mapping

        // example: if we have seed 5, then the index 5 of the array should give an offset, lets say -2, then the result position is 3. 

        var maxArraySize = 2121546655;

        // parse input
        long[] seeds = input[0].Split(':').Last().Trim().Split(' ').Select(long.Parse).ToArray();

        // 0 : Seed to soil
        // 1 : Soil to fertilizer
        // 2 : Fertilizer to water
        var currentMap = 0;
        var distanceMaps = new List<DistanceMap>();

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

                // TODO:
                var distanceMap = new DistanceMap
                {
                    Type = currentMap,
                    Elements = new List<DistanceMapElement>
                    {
                        new()
                        {
                            Type = currentMap,
                            DestinationRangeStart = destinationRangeStart,
                            SourceRangeStart = sourceRangeStart,
                            RangeLength = rangeLength
                        }
                    }
                    // DestinationRangeStart = destinationRangeStart,
                    // SourceRangeStart = sourceRangeStart,
                    // RangeLength = rangeLength
                };

                distanceMaps.Add(distanceMap);
            }
        }

        var lowestLocation = long.MaxValue;
        for (var i = 0; i < seeds.Length; i++)
        {
            // If it does note exist, it´s one to one

            long offset = GetOffset(seeds[i], distanceMaps);
            // long soil = seedToSoilMap[seed] == 0 ? seed : seedToSoilMap[seed];
            // long fertilizer = soilToFertilizerMap[soil] == 0 ? soil : soilToFertilizerMap[soil];
            // long water = fertilizerToWaterMap[fertilizer] == 0 ? fertilizer : fertilizerToWaterMap[fertilizer];
            // long light = waterToLightMap[water] == 0 ? water : waterToLightMap[water];
            // long temperature = lightToTemperatureMap[light] == 0 ? light : lightToTemperatureMap[light];
            // long humidity = tempratureToHumidityMap[temperature] == 0 ? temperature : tempratureToHumidityMap[temperature];
            // long location = humidityToLocationMap[humidity] == 0 ? humidity : humidityToLocationMap[humidity];

            // long soil = seedToSoilDictionary.TryGetValue(seed, out long value) ? value : seed;
            // long fertilizer = soilToFertilizerDictionary.TryGetValue(soil, out value) ? value : soil;
            // long water = fertilizerToWaterDictionary.TryGetValue(fertilizer, out value) ? value : fertilizer;
            // long light = waterToLightDictionary.TryGetValue(water, out value) ? value : water;
            // long temperature = lightToTemperatureDictionary.TryGetValue(light, out value) ? value : light;
            // long humidity = tempratureToHumidityDictionary.TryGetValue(temperature, out value) ? value : temperature;
            // long location = humidityToLocationDictionary.TryGetValue(humidity, out value) ? value : humidity;

            // _logger.LogInformation("Seed {Seed} -> Soil {Soil} -> Fertilizer {Fertilizer} -> Water {Water} -> Light {Light} -> Temperature {Temperature} -> Humidity {Humidity} -> Light {Light2}", seed, soil, fertilizer, water, light, temperature, humidity, location);
            //
            lowestLocation = Math.Min(lowestLocation, offset);
        }

        return lowestLocation;
    }

    private long GetOffset(long seed, List<DistanceMap> distanceMaps)
    {
        long offset = 0;
        foreach (var distanceMap in distanceMaps)
        {
            if (seed >= distanceMap.SourceRangeStart && seed <= distanceMap.SourceRangeStart + distanceMap.RangeLength)
            {
                offset = distanceMap.DestinationRangeStart - distanceMap.SourceRangeStart;
            }
        }

        return offset;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 5 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}