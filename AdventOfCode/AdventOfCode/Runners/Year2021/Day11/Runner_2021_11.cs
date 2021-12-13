using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day11
{
  /// <summary>
  /// The runner_2021_11.
  /// </summary>
  public class Runner_2021_11
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_11"/> class.
    /// </summary>
    public Runner_2021_11()
      : base(2021, 11)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var grid = new Grid(inputLines);

      for (int i = 0; i < 100; i++)
      {
        grid.Update();
      }

      ulong part1 = grid.TotalFlashes;
      while (!grid.EveryoneFlashed)
      {
        grid.Update();
      }

      ulong part2 = grid.TotalStepsTaken;

      return (part1, part2);
    }
  }
}