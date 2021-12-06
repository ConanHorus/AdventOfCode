using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_18.
  /// </summary>
  public class Runner_2015_18
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_18"/> class.
    /// </summary>
    public Runner_2015_18()
      : base(2015, 18)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var grid = new Grid(inputLines, new Dictionary<(bool alive, int neighbors), bool>
      {
        { (false, 0), false }, { (true, 0), false },
        { (false, 1), false }, { (true, 1), false },
        { (false, 2), false }, { (true, 2), true },
        { (false, 3), true }, { (true, 3), true },
        { (false, 4), false }, { (true, 4), false },
        { (false, 5), false }, { (true, 5), false },
        { (false, 6), false }, { (true, 6), false },
        { (false, 7), false }, { (true, 7), false },
        { (false, 8), false }, { (true, 8), false },
      });

      for (int i = 0; i < 100; i++)
      {
        grid.Update();
      }

      int part1 = grid.CountLivingCells();

      grid = new Grid(inputLines, new Dictionary<(bool alive, int neighbors), bool>
      {
        { (false, 0), false }, { (true, 0), false },
        { (false, 1), false }, { (true, 1), false },
        { (false, 2), false }, { (true, 2), true },
        { (false, 3), true }, { (true, 3), true },
        { (false, 4), false }, { (true, 4), false },
        { (false, 5), false }, { (true, 5), false },
        { (false, 6), false }, { (true, 6), false },
        { (false, 7), false }, { (true, 7), false },
        { (false, 8), false }, { (true, 8), false },
      });

      for (int i = 0; i < 100; i++)
      {
        grid.Update();
        grid.KeepCornersOn();
      }

      int part2 = grid.CountLivingCells();

      return (part1, part2);
    }

    /// <summary>
    /// The grid.
    /// </summary>
    private class Grid
    {
      /// <summary>
      /// Buffer 1.
      /// </summary>
      private readonly GridBuffer buffer1;

      /// <summary>
      /// Buffer 2.
      /// </summary>
      private readonly GridBuffer buffer2;

      /// <summary>
      /// Rules.
      /// </summary>
      private readonly Dictionary<(bool alive, int neighbors), bool> rules;

      /// <summary>
      /// Main buffer.
      /// </summary>
      private GridBuffer mainBuffer;

      /// <summary>
      /// Next buffer.
      /// </summary>
      private GridBuffer nextBuffer;

      /// <summary>
      /// Initializes a new instance of the <see cref="Grid"/> class.
      /// </summary>
      /// <param name="inputLines">The input lines.</param>
      public Grid(string[] inputLines, Dictionary<(bool alive, int neighbors), bool> rules)
      {
        int width = inputLines[0].Length;
        int height = inputLines.Length;
        this.buffer1 = new GridBuffer(width, height);
        this.buffer2 = new GridBuffer(width, height);

        this.mainBuffer = this.buffer1;
        this.nextBuffer = this.buffer2;
        this.mainBuffer.LoadStateFromInput(inputLines);

        this.rules = rules;
      }

      /// <summary>
      /// Updates the grid.
      /// </summary>
      public void Update()
      {
        for (int x = 0; x < this.mainBuffer.Width; x++)
        {
          for (int y = 0; y < this.mainBuffer.Height; y++)
          {
            bool alive = this.mainBuffer.ProbeLocation(x, y);
            int neighbors = this.mainBuffer.CountNeighbors(x, y);
            alive = this.rules[(alive, neighbors)];
            this.nextBuffer.SetCell(x, y, alive);
          }
        }

        var holder = this.mainBuffer;
        this.mainBuffer = this.nextBuffer;
        this.nextBuffer = holder;
      }

      /// <summary>
      /// Keeps the corners on.
      /// </summary>
      public void KeepCornersOn()
      {
        this.mainBuffer.SetCell(0, 0, true);
        this.mainBuffer.SetCell(0, this.mainBuffer.Height - 1, true);
        this.mainBuffer.SetCell(this.mainBuffer.Width - 1, this.mainBuffer.Height - 1, true);
        this.mainBuffer.SetCell(this.mainBuffer.Width - 1, 0, true);
      }

      /// <summary>
      /// Counts the living cells.
      /// </summary>
      /// <returns>An int.</returns>
      public int CountLivingCells()
      {
        return this.mainBuffer.CountLivingCells();
      }
    }

    /// <summary>
    /// The grid buffer.
    /// </summary>
    private class GridBuffer
    {
      /// <summary>
      /// Width.
      /// </summary>
      public readonly int Width;

      /// <summary>
      /// Height.
      /// </summary>
      public readonly int Height;

      /// <summary>
      /// Buffer.
      /// </summary>
      private readonly bool[,] buffer;

      /// <summary>
      /// Initializes a new instance of the <see cref="GridBuffer"/> class.
      /// </summary>
      /// <param name="width">The width.</param>
      /// <param name="height">The height.</param>
      public GridBuffer(int width, int height)
      {
        this.buffer = new bool[width, height];
        this.Width = width;
        this.Height = height;
      }

      /// <summary>
      /// Loads the state from input.
      /// </summary>
      /// <param name="input">The input.</param>
      public void LoadStateFromInput(string[] input)
      {
        for (int y = 0; y < input.Length; y++)
        {
          for (int x = 0; x < input[y].Length; x++)
          {
            this.buffer[x, y] = input[y][x] == '#';
          }
        }
      }

      /// <summary>
      /// Counts the neighbors.
      /// </summary>
      /// <param name="x">The x.</param>
      /// <param name="y">The y.</param>
      /// <returns>An int.</returns>
      public int CountNeighbors(int x, int y)
      {
        int count = 0;

        for (int xx = x - 1; xx <= x + 1; xx++)
        {
          for (int yy = y - 1; yy <= y + 1; yy++)
          {
            if (xx == x && yy == y)
            {
              continue;
            }

            count += this.ProbeLocation(xx, yy) ? 1 : 0;
          }
        }

        return count;
      }

      /// <summary>
      /// Sets the cell.
      /// </summary>
      /// <param name="x">The x.</param>
      /// <param name="y">The y.</param>
      /// <param name="alive">If true, alive.</param>
      public void SetCell(int x, int y, bool alive)
      {
        this.buffer[x, y] = alive;
      }

      /// <summary>
      /// Counts the living cells.
      /// </summary>
      /// <returns>An int.</returns>
      public int CountLivingCells()
      {
        int count = 0;

        for (int x = 0; x < this.Width; x++)
        {
          for (int y = 0; y < this.Height; y++)
          {
            count += this.buffer[x, y] ? 1 : 0;
          }
        }

        return count;
      }

      /// <summary>
      /// Probes the location.
      /// </summary>
      /// <param name="x">The x.</param>
      /// <param name="y">The y.</param>
      /// <returns>A bool.</returns>
      public bool ProbeLocation(int x, int y)
      {
        if (x < 0 || x >= this.Width)
        {
          return false;
        }

        if (y < 0 || y >= this.Height)
        {
          return false;
        }

        return this.buffer[x, y];
      }
    }
  }
}