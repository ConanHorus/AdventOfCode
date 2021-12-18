using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_6.
  /// </summary>
  public class Runner_2015_06
    : RunnerBase
  {
    /// <summary>
    /// Parser chain.
    /// </summary>
    private static readonly ParserChain parser = new ParserChain();

    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_06"/> class.
    /// </summary>
    public Runner_2015_06()
      : base(2015, 6)
    {
    }

    /// <summary>
    /// The instruction enum.
    /// </summary>
    private enum InstructionEnum
    {
      /// <summary>
      /// Turn on lights.
      /// </summary>
      On,

      /// <summary>
      /// Turn off lights.
      /// </summary>
      Off,

      /// <summary>
      /// Toggle lights.
      /// </summary>
      Toggle
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var grid = new Grid();
      var elvishGrid = new ElvishGrid();
      var instructions = inputLines.Select(x => new Instruction(x));
      foreach (var instruction in instructions)
      {
        grid.PerformInstruction(instruction);
        elvishGrid.PerformInstruction(instruction);
      }

      return (grid.CountTotalLightsOn(), elvishGrid.CountTotalLightsOn());
    }

    /// <summary>
    /// Instruction.
    /// </summary>
    private struct Instruction
    {
      /// <summary>
      /// Gets the action.
      /// </summary>
      public InstructionEnum Action { get; }

      /// <summary>
      /// Gets the rectangle.
      /// </summary>
      public Rectangle Rectangle { get; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Instruction"/> class.
      /// </summary>
      /// <param name="instruction">The instruction.</param>
      public Instruction(string instruction)
      {
        string[] parts = instruction.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        (this.Action, this.Rectangle) = parser.Parse(parts);
      }
    }

    /// <summary>
    /// The Rectangle.
    /// </summary>
    private struct Rectangle
    {
      /// <summary>
      /// Gets the x.
      /// </summary>
      public int X { get; }

      /// <summary>
      /// Gets the y.
      /// </summary>
      public int Y { get; }

      /// <summary>
      /// Gets the width.
      /// </summary>
      public int Width { get; }

      /// <summary>
      /// Gets the height.
      /// </summary>
      public int Height { get; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Rectangle"/> class.
      /// </summary>
      /// <param name="corner1">The corner1.</param>
      /// <param name="corner2">The corner2.</param>
      public Rectangle(string corner1, string corner2)
      {
        int[] cornerInts1 = corner1.Split(',').Select(x => int.Parse(x)).ToArray();
        int[] cornerInts2 = corner2.Split(',').Select(x => int.Parse(x)).ToArray();

        this.X = Math.Min(cornerInts1[0], cornerInts2[0]);
        this.Y = Math.Min(cornerInts1[1], cornerInts2[1]);
        this.Width = Math.Max(cornerInts1[0], cornerInts2[0]) - this.X;
        this.Height = Math.Max(cornerInts1[1], cornerInts2[1]) - this.Y;
      }

      /// <summary>
      /// Enumerates over all points.
      /// </summary>
      /// <returns>Enumerated points.</returns>
      public IEnumerable<(int x, int y)> Enumerate()
      {
        int x = this.X;
        int y = this.Y;

        while (y <= this.Height + this.Y)
        {
          while (x <= this.Width + this.X)
          {
            yield return (x, y);
            x++;
          }

          y++;
          x = this.X;
        }
      }
    }

    /// <summary>
    /// The grid.
    /// </summary>
    private class Grid
    {
      /// <summary>
      /// Data.
      /// </summary>
      private readonly bool[,] data;

      /// <summary>
      /// Initializes a new instance of the <see cref="Grid"/> class.
      /// </summary>
      public Grid()
      {
        this.data = new bool[1000, 1000];
        for (int x = 0; x < 1000; x++)
        {
          for (int y = 0; y < 1000; y++)
          {
            data[x, y] = false;
          }
        }
      }

      /// <summary>
      /// Performs instruction.
      /// </summary>
      /// <param name="instruction">Instruction.</param>
      public void PerformInstruction(Instruction instruction)
      {
        if (instruction.Action == InstructionEnum.On)
        {
          this.PerformOn(instruction.Rectangle);
        }

        if (instruction.Action == InstructionEnum.Off)
        {
          this.PerformOff(instruction.Rectangle);
        }

        if (instruction.Action == InstructionEnum.Toggle)
        {
          this.PerformToggle(instruction.Rectangle);
        }
      }

      /// <summary>
      /// Counts total lights on.
      /// </summary>
      /// <returns>Total lights on.</returns>
      public int CountTotalLightsOn()
      {
        int total = 0;

        for (int x = 0; x < 1000; x++)
        {
          for (int y = 0; y < 1000; y++)
          {
            total += this.data[x, y] ? 1 : 0;
          }
        }

        return total;
      }

      /// <summary>
      /// Performs on.
      /// </summary>
      /// <param name="rectangle">Rectangle.</param>
      private void PerformOn(Rectangle rectangle)
      {
        foreach ((int x, int y) in rectangle.Enumerate())
        {
          this.data[x, y] = true;
        }
      }

      /// <summary>
      /// Performs off.
      /// </summary>
      /// <param name="rectangle">Rectangle.</param>
      private void PerformOff(Rectangle rectangle)
      {
        foreach ((int x, int y) in rectangle.Enumerate())
        {
          this.data[x, y] = false;
        }
      }

      /// <summary>
      /// Performs toggle.
      /// </summary>
      /// <param name="rectangle">Rectangle.</param>
      private void PerformToggle(Rectangle rectangle)
      {
        foreach ((int x, int y) in rectangle.Enumerate())
        {
          this.data[x, y] ^= true;
        }
      }
    }

    /// <summary>
    /// The elvish grid.
    /// </summary>
    private class ElvishGrid
    {
      /// <summary>
      /// Data.
      /// </summary>
      private readonly int[,] data;

      /// <summary>
      /// Initializes a new instance of the <see cref="ElvishGrid"/> class.
      /// </summary>
      public ElvishGrid()
      {
        this.data = new int[1000, 1000];
        for (int x = 0; x < 1000; x++)
        {
          for (int y = 0; y < 1000; y++)
          {
            data[x, y] = 0;
          }
        }
      }

      /// <summary>
      /// Performs instruction.
      /// </summary>
      /// <param name="instruction">Instruction.</param>
      public void PerformInstruction(Instruction instruction)
      {
        if (instruction.Action == InstructionEnum.On)
        {
          this.PerformOn(instruction.Rectangle);
        }

        if (instruction.Action == InstructionEnum.Off)
        {
          this.PerformOff(instruction.Rectangle);
        }

        if (instruction.Action == InstructionEnum.Toggle)
        {
          this.PerformToggle(instruction.Rectangle);
        }
      }

      /// <summary>
      /// Counts total lights on.
      /// </summary>
      /// <returns>Total lights on.</returns>
      public long CountTotalLightsOn()
      {
        long total = 0;

        for (int x = 0; x < 1000; x++)
        {
          for (int y = 0; y < 1000; y++)
          {
            total += this.data[x, y];
          }
        }

        return total;
      }

      /// <summary>
      /// Performs on.
      /// </summary>
      /// <param name="rectangle">Rectangle.</param>
      private void PerformOn(Rectangle rectangle)
      {
        foreach ((int x, int y) in rectangle.Enumerate())
        {
          this.data[x, y]++;
        }
      }

      /// <summary>
      /// Performs off.
      /// </summary>
      /// <param name="rectangle">Rectangle.</param>
      private void PerformOff(Rectangle rectangle)
      {
        foreach ((int x, int y) in rectangle.Enumerate())
        {
          this.data[x, y] = Math.Max(0, this.data[x, y] - 1);
        }
      }

      /// <summary>
      /// Performs toggle.
      /// </summary>
      /// <param name="rectangle">Rectangle.</param>
      private void PerformToggle(Rectangle rectangle)
      {
        foreach ((int x, int y) in rectangle.Enumerate())
        {
          this.data[x, y] += 2;
        }
      }
    }

    /// <summary>
    /// The parser chain.
    /// </summary>
    private class ParserChain
    {
      /// <summary>
      /// Parsers.
      /// </summary>
      private readonly Parser[] parsers =
      {
        new OnParser(),
        new OffParser(),
        new ToggleParser()
      };

      /// <summary>
      /// Parses instruction parts.
      /// </summary>
      /// <param name="parts">Parts.</param>
      /// <returns>Instruction parsed.</returns>
      public (InstructionEnum, Rectangle) Parse(string[] parts)
      {
        foreach (var parser in this.parsers)
        {
          var parsed = parser.Parse(parts);
          if (parsed is not null)
          {
            return parsed.Value;
          }
        }

        throw new NotImplementedException();
      }
    }

    /// <summary>
    /// The parser.
    /// </summary>
    private abstract class Parser
    {
      /// <summary>
      /// Parses instruction parts.
      /// </summary>
      /// <param name="parts">Parts.</param>
      /// <returns>Instruction parsed.</returns>
      public abstract (InstructionEnum, Rectangle)? Parse(string[] parts);
    }

    /// <summary>
    /// The on parser.
    /// </summary>

    private class OnParser
      : Parser
    {
      /// <inheritdoc/>
      public override (InstructionEnum, Rectangle)? Parse(string[] parts)
      {
        if (parts[1] != "on")
        {
          return null;
        }

        return (InstructionEnum.On, new Rectangle(parts[2], parts[4]));
      }
    }

    /// <summary>
    /// The off parser.
    /// </summary>
    private class OffParser
      : Parser
    {
      /// <inheritdoc/>
      public override (InstructionEnum, Rectangle)? Parse(string[] parts)
      {
        if (parts[1] != "off")
        {
          return null;
        }

        return (InstructionEnum.Off, new Rectangle(parts[2], parts[4]));
      }
    }

    /// <summary>
    /// The toggle parser.
    /// </summary>
    private class ToggleParser
      : Parser
    {
      /// <inheritdoc/>
      public override (InstructionEnum, Rectangle)? Parse(string[] parts)
      {
        if (parts[0] != "toggle")
        {
          return null;
        }

        return (InstructionEnum.Toggle, new Rectangle(parts[1], parts[3]));
      }
    }
  }
}