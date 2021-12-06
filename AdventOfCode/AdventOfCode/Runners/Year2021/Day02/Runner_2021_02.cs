using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day02
{
  /// <summary>
  /// The runner_2021_02.
  /// </summary>
  public class Runner_2021_02
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_02"/> class.
    /// </summary>
    public Runner_2021_02()
      : base(2021, 2)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var sub = new Sub();
      var subWithAim = new Sub(true);
      foreach (var action in inputLines)
      {
        sub.PerformAction(action);
        subWithAim.PerformAction(action);
      }

      int part1 = sub.Forward * sub.Depth;
      int part2 = subWithAim.Forward * subWithAim.Depth;

      return (part1, part2);
    }
  }
}