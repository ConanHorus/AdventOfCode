using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day07
{
  /// <summary>
  /// The runner_2021_07.
  /// </summary>
  public class Runner_2021_07
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_07"/> class.
    /// </summary>
    public Runner_2021_07()
      : base(2021, 7)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      int[] xCoords = inputString.Split(',').Select(x => int.Parse(x)).ToArray();
      var coordCounts = new Dictionary<int, int>();
      foreach (int xCoord in xCoords)
      {
        if (!coordCounts.ContainsKey(xCoord))
        {
          coordCounts[xCoord] = 0;
        }

        coordCounts[xCoord]++;
      }

      var fuel1 = new Dictionary<int, long>();
      var fuel2 = new Dictionary<int, long>();
      for (int xCoord = xCoords.Min(); xCoord < xCoords.Max(); xCoord++)
      {
        fuel1[xCoord] = coordCounts.Select(x => Math.Abs(x.Key - xCoord) * x.Value).Sum();
        fuel2[xCoord] = coordCounts.Select(x =>
        {
          int move = Math.Abs(x.Key - xCoord);
          return ((move * (move + 1)) / 2) * x.Value;
        }).Sum();
      }

      long part1 = fuel1.Values.Min();
      long part2 = fuel2.Values.Min();

      return (part1, part2);
    }
  }
}