namespace AdventOfCode._2023.Day6;

public interface ISolutionService
{
    public int RunPart1(string[] input);
    public long RunPart2(string[] input);
}

public class SolutionService : ISolutionService
{
    private readonly ILogger<ISolutionService> _logger;

    public SolutionService(ILogger<SolutionService> logger)
    {
        _logger = logger;
    }

    public int RunPart1(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 6 - Part 1");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        int[] time = input[0].Split(':').Last().Split(' ').Select(x => int.TryParse(x, out var result) ? result : 0).Where(x => x != 0).ToArray();
        int[] distance = input[1].Split(':').Last().Split(' ').Select(x => int.TryParse(x, out var result) ? result : 0).Where(x => x != 0).ToArray();

        int[] winningOptions = new int[time.Length];

        for (var r = 0; r < time.Length; r++)
        {
            _logger.LogInformation("Race {Race} - Time: {Time} - Distance: {Distance}", r, time[r], distance[r]);

            for (var i = 0; i < time[r]; i++)
            {
                int hold = i;
                int raceTime = time[r] - i;

                int distanceCovered = hold * raceTime;

                if (distanceCovered > distance[r])
                {
                    winningOptions[r]++;
                }

                _logger.LogInformation("Hold {Hold} ms, distance covered: {DistanceCovered}", hold, distanceCovered);
            }
        }


        // multiply all the winning options together
        int result = winningOptions.Aggregate(1, (x, y) => x * y);

        return result;
    }

    public long RunPart2(string[] input)
    {
        _logger.LogInformation("Solving - 2023 - Day 6 - Part 2");
        _logger.LogInformation("Input contains {Input} values", input.Length);

        string timeString = input[0].Split(':').Last().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Aggregate("", (current, t) => current + t);
        string distanceString = input[1].Split(':').Last().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Aggregate("", (current, t) => current + t);

        long time = long.Parse(timeString);
        long distance = long.Parse(distanceString);

        long winningOptions = 0;

        _logger.LogInformation("Race {Race} - Time: {Time} - Distance: {Distance}", 0, time, distance);

        // Parallel.For(0, time, i =>
        // {
        //     long hold = i;
        //     long raceTime = time - i;
        //
        //     long distanceCovered = hold * raceTime;
        //
        //     if (distanceCovered > distance)
        //     {
        //         winningOptions++;
        //     }
        // });

        // for (var i = 0; i < time; i++)
        // {
        //     if (i * (time - i) > distance)
        //     {
        //         winningOptions++;
        //     }
        // }


        for (var i = 0; i < time; i++)
        {
            long hold = i;
            long raceTime = time - i;

            long distanceCovered = hold * raceTime;

            if (distanceCovered > distance)
            {
                winningOptions++;
            }

            // _logger.LogInformation("Hold {Hold} ms, distance covered: {DistanceCovered}", hold, distanceCovered);
        }

        return winningOptions;
    }
}