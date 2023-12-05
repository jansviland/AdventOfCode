namespace AdventOfCode._2023.Day5;

public interface ISolutionService
{
    public long RunPart1(string[] input);
    public int RunPart2(string[] input);
    public long GetNewPosition(long position, DistanceMap distanceMap);
}

enum Type
{
    Soil = 0,
    Fertilizer = 1,
    Water = 2,
    Light = 3,
    Temprature = 4,
    Humidity = 5,
    Location = 6
}

public class DistanceMap
{
    /// <summary>
    /// 0 : Seed to soil
    /// 1 : Soil to fertilizer
    /// 2 : Fertilizer to water
    /// etc
    /// </summary>
    public int Type { get; set; }
    public List<DistanceMapping> Mapping { get; set; }
}

public class DistanceMapping
{
    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public long RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 5 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        // parse input
        long[] seeds = input[0].Split(':').Last().Trim().Split(' ').Select(long.Parse).ToArray();

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

                // create distance map if it does not exist
                if (distanceMaps.Any(x => x.Type == currentMap) == false)
                {
                    distanceMaps.Add(new DistanceMap
                    {
                        Type = currentMap,
                        Mapping = new List<DistanceMapping>()
                    });
                }

                // add mapping
                distanceMaps.First(x => x.Type == currentMap).Mapping.Add(new DistanceMapping
                {
                    DestinationRangeStart = destinationRangeStart,
                    SourceRangeStart = sourceRangeStart,
                    RangeLength = rangeLength
                });
            }
        }

        var sb = new StringBuilder();

        var lowestLocation = long.MaxValue;
        for (var i = 0; i < seeds.Length; i++)
        {
            var startPosition = seeds[i];

            sb.Append($"Seed {startPosition}, ");

            // If it does note exist, it´s one to one
            foreach (var distanceMap in distanceMaps)
            {
                long endPosition = GetNewPosition(startPosition, distanceMap);

                var type = (Type)distanceMap.Type;
                sb.Append($"{type} {endPosition}, ");

                startPosition = endPosition;
            }

            lowestLocation = Math.Min(lowestLocation, startPosition);

            _logger.LogInformation(sb.ToString());
            sb.Clear();
        }

        return lowestLocation;
    }

    public long GetNewPosition(long position, DistanceMap distanceMap)
    {
        foreach (var distanceMapping in distanceMap.Mapping)
        {
            if (position >= distanceMapping.SourceRangeStart && position < distanceMapping.SourceRangeStart + distanceMapping.RangeLength)
            {
                var diff = distanceMapping.DestinationRangeStart - distanceMapping.SourceRangeStart;
                return position + diff;
            }
        }

        return position;
    }

    public int RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 5 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        throw new NotImplementedException();
    }
}