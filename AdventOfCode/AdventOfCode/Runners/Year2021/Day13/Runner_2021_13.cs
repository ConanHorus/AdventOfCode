using System;

namespace AdventOfCode.Runners.Year2021.Day13
{
  /// <summary>
  /// The runner_2021_13.
  /// </summary>
  public class Runner_2021_13
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_13"/> class.
    /// </summary>
    public Runner_2021_13()
      : base(2021, 13)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var paper = new Paper(inputLines);

      int? part1 = null;
      foreach (string line in inputLines)
      {
        string[] parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
          continue;
        }

        if (parts[0].EndsWith("x"))
        {
          paper.FoldLeftVertically(int.Parse(parts[1]));
        }

        if (parts[0].EndsWith("y"))
        {
          paper.FoldUpHorizontally(int.Parse(parts[1]));
        }

        if (part1 is null)
        {
          part1 = paper.VisibleDots;
        }
      }

      paper.Print();
      return (part1, null);
    }
  }
}