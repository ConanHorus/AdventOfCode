using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day08
{
  /// <summary>
  /// The runner_2021_08.
  /// </summary>
  public class Runner_2021_08
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_08"/> class.
    /// </summary>
    public Runner_2021_08()
      : base(2021, 8)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var lines = inputLines.Select(x => new Line(x));

      int part1 = 0;
      int part2 = 0;
      foreach (var line in lines)
      {
        part1 += line.CountOccurancesInOutput(1);
        part1 += line.CountOccurancesInOutput(4);
        part1 += line.CountOccurancesInOutput(7);
        part1 += line.CountOccurancesInOutput(8);

        part2 += line.GetOutputValue();
      }

      return (part1, part2);
    }
  }
}