using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_5.
  /// </summary>
  public class Runner_2015_05
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_05"/> class.
    /// </summary>
    public Runner_2015_05()
      : base(2015, 5)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var data = inputLines.Select(x => this.ParseLine(x)).ToArray();
      int nice1 = data.Where(x => x.IsNice1).Count();
      int nice2 = data.Where(x => x.IsNice2).Count();

      return (nice1, nice2);
    }

    /// <summary>
    /// Parses the line.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <returns>Line data.</returns>
    private LineData ParseLine(string line)
    {
      char[] buffer = { (char)0, (char)0, (char)0 };
      var data = new LineData();
      var lastDouble = (str: "\0\0", count: 0);
      var doubles = new HashSet<string>();
      foreach (char c in line)
      {
        buffer[0] = buffer[1];
        buffer[1] = buffer[2];
        buffer[2] = c;
        string trippleString = new string(buffer);
        string doubleString = trippleString.Substring(1);

        switch (c)
        {
          case 'a':
          case 'e':
          case 'i':
          case 'o':
          case 'u':
            data.Vowels++;
            break;
        }

        if (buffer[1] == buffer[2])
        {
          data.DoubleLetter = true;
        }

        switch (doubleString)
        {
          case "ab":
          case "cd":
          case "pq":
          case "xy":
            data.ContainsNaughtyString = true;
            break;
        }

        if (buffer[0] == buffer[2])
        {
          data.BifercatedDouble = true;
        }

        if (doubles.Contains(doubleString))
        {
          if (lastDouble.str != doubleString)
          {
            data.DoubleDouble = true;
          }

          if (lastDouble.str == doubleString && lastDouble.count > 0)
          {
            data.DoubleDouble = true;
          }
        }

        if (!doubles.Contains(doubleString))
        {
          doubles.Add(doubleString);
        }

        if (lastDouble.str == doubleString)
        {
          lastDouble.count++;
        }
        else
        {
          lastDouble.str = doubleString;
          lastDouble.count = 0;
        }
      }

      return data;
    }

    /// <summary>
    /// The line data.
    /// </summary>
    private class LineData
    {
      /// <summary>
      /// Gets or sets the vowels.
      /// </summary>
      public int Vowels { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether double letter.
      /// </summary>
      public bool DoubleLetter { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether contains naughty string.
      /// </summary>
      public bool ContainsNaughtyString { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether bifercated double.
      /// </summary>
      public bool BifercatedDouble { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether double double.
      /// </summary>
      public bool DoubleDouble { get; set; }

      /// <summary>
      /// Gets a value indicating whether is nice 1.
      /// </summary>
      public bool IsNice1 => this.Vowels >= 3 && this.DoubleLetter && !this.ContainsNaughtyString;

      /// <summary>
      /// Gets a value indicating whether is nice 2.
      /// </summary>
      public bool IsNice2 => this.BifercatedDouble && this.DoubleDouble;
    }
  }
}