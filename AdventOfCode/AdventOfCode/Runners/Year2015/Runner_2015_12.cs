using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_12.
  /// </summary>
  public class Runner_2015_12
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_12"/> class.
    /// </summary>
    public Runner_2015_12()
      : base(2015, 12)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var rootNode = CreateNodes(inputString);

      long part1 = rootNode.AddUpNumbers();
      long part2 = rootNode.AddUpNumbers(ignoreReds: true);

      return (part1, part2);
    }

    /// <summary>
    /// Creates the nodes.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A Node.</returns>
    private static Node CreateNodes(string input, int ptr = 0)
    {
      int level = -1;
      var currentNode = new Node();
      long number = 0;
      bool negative = false;

      for (; ptr < input.Length; ptr++)
      {
        char c = input[ptr];
        if (c == '{' || c == '[')
        {
          if (level == -1 && c == '{')
          {
            currentNode.Class = true;
          }

          level++;

          if (level == 1)
          {
            currentNode.Children.Add(CreateNodes(input, ptr));
          }
        }

        if (c == '}' || c == ']')
        {
          level--;

          if (level == -1)
          {
            if (number != 0)
            {
              currentNode.Numbers.Add(SumUpNumber());
            }

            return currentNode;
          }
        }

        if (level > 0)
        {
          continue;
        }

        if (c == 'r' && input[ptr + 1] == 'e' && input[ptr + 2] == 'd')
        {
          currentNode.ContainsRed = true;
        }

        if (c >= '0' && c <= '9')
        {
          number *= 10;
          number += c - '0';
          continue;
        }

        if (c == '-')
        {
          negative = true;
          continue;
        }

        if (number != 0)
        {
          currentNode.Numbers.Add(SumUpNumber());
        }
      }

      return currentNode;

      long SumUpNumber()
      {
        if (negative)
        {
          number *= -1;
        }

        long val = number;
        number = 0;
        negative = false;

        return val;
      }
    }

    /// <summary>
    /// The node.
    /// </summary>
    private class Node
    {
      /// <summary>
      /// Gets or sets a value indicating whether contains red.
      /// </summary>
      public bool ContainsRed { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether class.
      /// </summary>
      public bool Class { get; set; }

      /// <summary>
      /// Gets or sets the children.
      /// </summary>
      public List<Node> Children { get; set; } = new List<Node>();

      /// <summary>
      /// Gets or sets the numbers.
      /// </summary>
      public List<long> Numbers { get; set; } = new List<long>();

      /// <summary>
      /// Adds the up numbers.
      /// </summary>
      /// <param name="ignoreReds">If true, ignore reds.</param>
      /// <returns>A long.</returns>
      public long AddUpNumbers(bool ignoreReds = false)
      {
        if (ignoreReds && this.ContainsRed && this.Class)
        {
          return 0;
        }

        return this.Numbers.Sum() + this.Children.Select(x => x.AddUpNumbers(ignoreReds)).Sum();
      }

      /// <summary>
      /// Writes the to console.
      /// </summary>
      /// <param name="level">The level.</param>
      public void WriteToConsole(int level)
      {
        WriteIndentedLine(level, $"isRed: {this.ContainsRed}, isClass: {this.Class}, total: {this.AddUpNumbers()}");
        foreach (long number in this.Numbers)
        {
          WriteIndentedLine(level, number.ToString());
        }

        foreach (var node in this.Children)
        {
          node.WriteToConsole(level + 1);
        }
      }

      /// <summary>
      /// Writes the indented line.
      /// </summary>
      /// <param name="level">The level.</param>
      /// <param name="v">The v.</param>
      private static void WriteIndentedLine(int level, string v)
      {
        var sb = new StringBuilder();
        foreach (string part in Enumerable.Repeat("| ", level))
        {
          sb.Append(part);
        }

        sb.Append(v);
        Console.WriteLine(sb.ToString());
      }
    }
  }
}