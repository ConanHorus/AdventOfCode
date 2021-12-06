using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_10.
  /// </summary>
  public class Runner_2015_10
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_10"/> class.
    /// </summary>
    public Runner_2015_10()
      : base(2015, 10)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      byte[] bytes = ToBytes(inputString);
      for (int i = 0; i < 40; i++)
      {
        bytes = Expand(bytes);
      }

      int part1 = bytes.Length;

      bytes = ToBytes(inputString);
      for (int i = 0; i < 50; i++)
      {
        bytes = Expand(bytes);
      }

      int part2 = bytes.Length;

      return (part1, part2);
    }

    /// <summary>
    /// Creates the string frim array.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    /// <returns>A string.</returns>
    private static string CreateStringFrimArray(byte[] bytes)
    {
      var sb = new StringBuilder();
      foreach (byte b in bytes)
      {
        sb.Append(b.ToString());
      }

      return sb.ToString();
    }

    /// <summary>
    /// Tos the bytes.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>An array of byte.</returns>
    private static byte[] ToBytes(string input)
    {
      return input.Select(x => (byte)(x - '0')).ToArray();
    }

    /// <summary>
    /// Expands the.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <returns>An array of byte.</returns>
    private static byte[] Expand(byte[] input)
    {
      var bytes = new List<byte>();
      int ptr = 0;
      byte current = input[ptr];
      byte counter = 0;
      while (ptr < input.Length)
      {
        byte b = input[ptr];
        if (b != current)
        {
          bytes.Add(counter);
          bytes.Add(current);
          counter = 0;
        }

        current = input[ptr];
        counter++;
        ptr++;
      }

      bytes.Add(counter);
      bytes.Add(current);

      return bytes.ToArray();
    }
  }
}