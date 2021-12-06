using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_1.
  /// </summary>
  public class Runner_2015_01
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_01"/> class.
    /// </summary>
    public Runner_2015_01()
      : base(2015, 1)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      int floor = 0;
      int firstBasement = 0;
      int index = 0;
      foreach (char c in inputString)
      {
        index++;
        if (c == '(')
        {
          floor++;
        }

        if (c == ')')
        {
          floor--;
        }

        if (floor < 0 && firstBasement == 0)
        {
          firstBasement = index;
        }
      }

      return (floor, firstBasement);
    }
  }
}