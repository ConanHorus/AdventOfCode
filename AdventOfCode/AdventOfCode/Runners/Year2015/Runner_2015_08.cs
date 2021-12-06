using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_8.
  /// </summary>
  public class Runner_2015_08
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_08"/> class.
    /// </summary>
    public Runner_2015_08()
      : base(2015, 8)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var details = new List<LineDetails>();

      foreach (string line in inputLines)
      {
        details.Add(new LineDetails { CodeLength = line.Length, StringLength = GetStringLength(line) });
      }

      int part1 = details.Sum(x => x.CodeLength - x.StringLength);

      details.Clear();

      foreach (string line in inputLines)
      {
        details.Add(new LineDetails { CodeLength = GetExpandedStringLength(line), StringLength = line.Length });
      }

      int part2 = details.Sum(x => x.CodeLength - x.StringLength);

      return (part1, part2);
    }

    /// <summary>
    /// Gets the expanded string length.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>An int.</returns>
    private static int GetExpandedStringLength(string line)
    {
      int count = 2;
      foreach (char c in line)
      {
        count++;
        if (c == '"' || c == '\\')
        {
          count++;
        }
      }

      return count;
    }

    /// <summary>
    /// Gets the string length.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>An int.</returns>
    private static int GetStringLength(string line)
    {
      int charPointer = 0;
      int count = -2;
      while (charPointer < line.Length)
      {
        count++;

        char c = line[charPointer++];
        if (c != '\\')
        {
          continue;
        }

        c = line[charPointer++];
        if (c != 'x')
        {
          continue;
        }

        charPointer += 2;
      }

      return count;
    }

    /// <summary>
    /// Line Details.
    /// </summary>
    private struct LineDetails
    {
      /// <summary>
      /// Gets or sets the code length.
      /// </summary>
      public int CodeLength { get; set; }

      /// <summary>
      /// Gets or sets the string length.
      /// </summary>
      public int StringLength { get; set; }
    }
  }
}