using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Runners.Year2021.Day05
{
  /// <summary>
  /// The runner_2021_05.
  /// </summary>
  public class Runner_2021_05
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_05"/> class.
    /// </summary>
    public Runner_2021_05()
      : base(2021, 5)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var linePoints = new Dictionary<Vector2, int>();
      var lines = inputLines.Select(x => new Line(x)).ToArray();
      foreach (var point in lines.Where(x => x.IsHorizontalOrVertical()).SelectMany(x => x.IterrateAllPoints()))
      {
        if (!linePoints.ContainsKey(point))
        {
          linePoints[point] = 0;
        }

        linePoints[point]++;
      }

      int part1 = linePoints.Values.Where(x => x >= 2).Count();

      linePoints.Clear();
      foreach (var point in lines.SelectMany(x => x.IterrateAllPoints()))
      {
        if (!linePoints.ContainsKey(point))
        {
          linePoints[point] = 0;
        }

        linePoints[point]++;
      }

      int part2 = linePoints.Values.Where(x => x >= 2).Count();

      return (part1, part2);
    }
  }
}