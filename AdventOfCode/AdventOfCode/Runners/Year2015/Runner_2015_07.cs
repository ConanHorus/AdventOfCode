using System;
using System.Collections.Generic;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_7.
  /// </summary>
  public class Runner_2015_07
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_07"/> class.
    /// </summary>
    public Runner_2015_07()
      : base(2015, 7)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var wires = new Dictionary<string, ushort?>();

      do
      {
        foreach (string line in inputLines)
        {
          Execute(line, wires);
        }
      }
      while (wires["a"] is null);

      ushort part1 = (ushort)wires["a"]!;
      wires.Clear();
      wires["b"] = part1;

      do
      {
        foreach (string line in inputLines)
        {
          Execute(line, wires);
        }
      }
      while (wires["a"] is null);

      return (part1, wires["a"]);
    }

    /// <summary>
    /// Executes the.
    /// </summary>
    /// <param name="line">The line.</param>
    /// <param name="wires">The wires.</param>
    private static void Execute(string line, Dictionary<string, ushort?> wires)
    {
      ushort? value1;
      ushort? value2;

      string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

      if (line.Contains("NOT"))
      {
        value1 = GetValue(parts[1], wires);
        Assign(parts[3], ExectuteNot(value1), wires);
        return;
      }

      if (line.Contains("LSHIFT"))
      {
        value1 = GetValue(parts[0], wires);
        value2 = GetValue(parts[2], wires);
        Assign(parts[4], ExecuteLeftShift(value1, value2), wires);
        return;
      }

      if (line.Contains("RSHIFT"))
      {
        value1 = GetValue(parts[0], wires);
        value2 = GetValue(parts[2], wires);
        Assign(parts[4], ExecuteRightShift(value1, value2), wires);
        return;
      }

      if (line.Contains("AND"))
      {
        value1 = GetValue(parts[0], wires);
        value2 = GetValue(parts[2], wires);
        Assign(parts[4], ExecuteAnd(value1, value2), wires);
        return;
      }

      if (line.Contains("OR"))
      {
        value1 = GetValue(parts[0], wires);
        value2 = GetValue(parts[2], wires);
        Assign(parts[4], ExecuteOr(value1, value2), wires);
        return;
      }

      Assign(parts[2], GetValue(parts[0], wires), wires);
    }

    /// <summary>
    /// Assigns the.
    /// </summary>
    /// <param name="wireName">The wire name.</param>
    /// <param name="value">The value.</param>
    /// <param name="wires">The wires.</param>
    private static void Assign(string wireName, ushort? value, Dictionary<string, ushort?> wires)
    {
      if (!wires.ContainsKey(wireName) || wires[wireName] is null)
      {
        wires[wireName] = value;
      }
    }

    /// <summary>
    /// Executes the or.
    /// </summary>
    /// <param name="value1">The value1.</param>
    /// <param name="value2">The value2.</param>
    /// <returns>An ushort? .</returns>
    private static ushort? ExecuteOr(ushort? value1, ushort? value2)
    {
      if (value1 is null || value2 is null)
      {
        return null;
      }

      return (ushort)(value1 | value2);
    }

    /// <summary>
    /// Executes the and.
    /// </summary>
    /// <param name="value1">The value1.</param>
    /// <param name="value2">The value2.</param>
    /// <returns>An ushort? .</returns>
    private static ushort? ExecuteAnd(ushort? value1, ushort? value2)
    {
      if (value1 is null || value2 is null)
      {
        return null;
      }

      return (ushort)(value1 & value2);
    }

    /// <summary>
    /// Executes the right shift.
    /// </summary>
    /// <param name="value1">The value1.</param>
    /// <param name="value2">The value2.</param>
    /// <returns>An ushort? .</returns>
    private static ushort? ExecuteRightShift(ushort? value1, ushort? value2)
    {
      if (value1 is null || value2 is null)
      {
        return null;
      }

      return (ushort)(value1 >> value2);
    }

    /// <summary>
    /// Executes the left shift.
    /// </summary>
    /// <param name="value1">The value1.</param>
    /// <param name="value2">The value2.</param>
    /// <returns>An ushort? .</returns>
    private static ushort? ExecuteLeftShift(ushort? value1, ushort? value2)
    {
      if (value1 is null || value2 is null)
      {
        return null;
      }

      return (ushort)(value1 << value2);
    }

    /// <summary>
    /// Exectutes the not.
    /// </summary>
    /// <param name="value1">The value1.</param>
    /// <returns>An ushort? .</returns>
    private static ushort? ExectuteNot(ushort? value1)
    {
      if (value1 is null)
      {
        return null;
      }

      return (ushort)~value1;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="part">The part.</param>
    /// <param name="wires">The wires.</param>
    /// <returns>An ushort? .</returns>
    private static ushort? GetValue(string part, Dictionary<string, ushort?> wires)
    {
      if (part[0] >= 'a' && part[0] <= 'z')
      {
        if (wires.TryGetValue(part, out ushort? val))
        {
          return val;
        }

        wires[part] = null;
        return null;
      }

      return ushort.Parse(part);
    }
  }
}