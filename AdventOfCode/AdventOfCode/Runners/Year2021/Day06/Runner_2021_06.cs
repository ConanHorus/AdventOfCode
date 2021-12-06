using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day06
{
  /// <summary>
  /// The runner_2021_06.
  /// </summary>
  public class Runner_2021_06
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_06"/> class.
    /// </summary>
    public Runner_2021_06()
      : base(2021, 6)
    {
    }

    /// <inheritdoc/>
    public override (string? part1, string? part2) Run(string inputString, string[] inputLines)
    {
      long part1 = 0;
      long part2 = 0;

      long[] fishByTimer = new long[9];

      var fish = inputString.Split(',').Select(x => int.Parse(x)).ToArray();
      foreach (int f in fish)
      {
        fishByTimer[f]++;
      }

      for (int day = 0; day < 256; day++)
      {
        long newFish = fishByTimer[0];
        long resetFish = fishByTimer[0];

        for (int i = 0; i < fishByTimer.Length - 1; i++)
        {
          fishByTimer[i] = fishByTimer[i + 1];
        }

        fishByTimer[8] = newFish;
        fishByTimer[6] += resetFish;

        if (day == 79)
        {
          part1 = fishByTimer.Sum();
        }
      }

      part2 = fishByTimer.Sum();

      return (part1.ToString(), part2.ToString());
    }
  }
}