using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day18
{
  /// <summary>
  /// The runner_2021_18.
  /// </summary>
  public class Runner_2021_18
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_18"/> class.
    /// </summary>
    public Runner_2021_18()
      : base(2021, 18)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var snailNumbers = RefreshTree(inputLines);
      var total = snailNumbers[0];
      for (int i = 1; i < snailNumbers.Length; i++)
      {
        total += snailNumbers[i];
      }

      long part1 = total.CalculateMagnitude();
      long part2 = FindLargestMagnitude(inputLines);

      return (part1, part2);
    }

    /// <summary>
    /// Refreshes the tree.
    /// </summary>
    /// <param name="inputLines">The input lines.</param>
    /// <returns>An array of SnailNumbers.</returns>
    private static SnailNumber[] RefreshTree(string[] inputLines)
    {
      return inputLines.Select(x => new SnailNumber(x)).ToArray();
    }

    /// <summary>
    /// Finds the largest magnitude.
    /// </summary>
    /// <param name="lines">The input lines.</param>
    /// <returns>A long.</returns>
    private static long FindLargestMagnitude(string[] lines)
    {
      long largest = 0;
      long magnitude;
      for (int i = 0; i < lines.Length; i++)
      {
        for (int j = 0; j < lines.Length; j++)
        {
          if (i == j)
          {
            continue;
          }

          magnitude = (new SnailNumber(lines[i]) + new SnailNumber(lines[j])).CalculateMagnitude();
          if (magnitude > largest)
          {
            largest = magnitude;
          }
        }
      }

      return largest;
    }
  }
}