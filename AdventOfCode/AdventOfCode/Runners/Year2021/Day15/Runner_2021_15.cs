using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day15
{
  /// <summary>
  /// The runner_2021_15.
  /// </summary>
  public class Runner_2021_15
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_15"/> class.
    /// </summary>
    public Runner_2021_15()
      : base(2021, 15)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var grid = new Grid(inputLines, fiveTimes: false);
      ulong part1 = grid.CalculateBestPathRisk();

      grid = new Grid(inputLines, fiveTimes: true);
      ulong part2 = grid.CalculateBestPathRisk();
      return (part1, part2);
    }
  }
}