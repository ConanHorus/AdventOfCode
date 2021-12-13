using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day11
{
  /// <summary>
  /// The grid.
  /// </summary>
  public class Grid
  {
    /// <summary>
    /// Data.
    /// </summary>
    private readonly byte[][] data;

    /// <summary>
    /// Locations to re-visit.
    /// </summary>
    private readonly Queue<(int x, int y)> revisit = new Queue<(int x, int y)>();

    /// <summary>
    /// Locations that have flashed.
    /// </summary>
    private readonly HashSet<(int x, int y)> hasFlashed = new HashSet<(int x, int y)>();

    /// <summary>
    /// Gets the total flashes.
    /// </summary>
    public ulong TotalFlashes { get; private set; }

    /// <summary>
    /// Gets the total steps taken.
    /// </summary>
    public ulong TotalStepsTaken { get; private set; }

    /// <summary>
    /// Gets a value indicating whether everyone flashed.
    /// </summary>
    public bool EveryoneFlashed
    {
      get
      {
        foreach (var pos in this.EnumerateAllPositions())
        {
          if (this[pos] != 0)
          {
            return false;
          }
        }

        return true;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Grid"/> class.
    /// </summary>
    /// <param name="lines">The lines.</param>
    public Grid(string[] lines)
    {
      this.data = new byte[lines.Length][];
      for (int iLine = 0; iLine < lines.Length; iLine++)
      {
        string line = lines[iLine];
        this.data[iLine] = new byte[line.Length];
        for (int iChar = 0; iChar < line.Length; iChar++)
        {
          this.data[iLine][iChar] = (byte)(line[iChar] - '0');
        }
      }
    }

    private byte this[(int x, int y) pos]
    {
      get
      {
        if (this.OutOfBounds(pos))
        {
          return 0;
        }

        return this.data[pos.x][pos.y];
      }

      set
      {
        if (this.OutOfBounds(pos))
        {
          return;
        }

        this.data[pos.x][pos.y] = value;
      }
    }

    /// <summary>
    /// Updates the.
    /// </summary>
    public void Update()
    {
      this.TotalStepsTaken++;

      this.revisit.Clear();
      this.hasFlashed.Clear();

      foreach (var pos in this.EnumerateAllPositions())
      {
        this.InrementEnergy(pos);
      }

      while (this.revisit.Count > 0)
      {
        var pos = this.revisit.Dequeue();
        if (this.hasFlashed.Contains(pos))
        {
          continue;
        }

        this.InrementEnergy(pos);
      }

      this.TotalFlashes += (ulong)this.hasFlashed.Count;
    }

    /// <summary>
    /// Adds the neighbors.
    /// </summary>
    /// <param name="pos">The pos.</param>
    /// <param name="revisit">The revisit.</param>
    private static void AddNeighbors((int x, int y) pos, Queue<(int x, int y)> revisit)
    {
      revisit.Enqueue((pos.x - 1, pos.y - 1));
      revisit.Enqueue((pos.x, pos.y - 1));
      revisit.Enqueue((pos.x + 1, pos.y - 1));

      revisit.Enqueue((pos.x - 1, pos.y));
      revisit.Enqueue((pos.x + 1, pos.y));

      revisit.Enqueue((pos.x - 1, pos.y + 1));
      revisit.Enqueue((pos.x, pos.y + 1));
      revisit.Enqueue((pos.x + 1, pos.y + 1));
    }

    /// <summary>
    /// Determines if postion is out of bounds.
    /// </summary>
    /// <param name="pos">The pos.</param>
    /// <returns>A bool.</returns>
    private bool OutOfBounds((int x, int y) pos)
    {
      return pos.x < 0 || pos.x >= this.data[0].Length || pos.y < 0 || pos.y >= this.data.Length;
    }

    /// <summary>
    /// Inrements the energy.
    /// </summary>
    /// <param name="pos">The pos.</param>
    private void InrementEnergy((int x, int y) pos)
    {
      this[pos]++;
      if (this[pos] > 9)
      {
        this[pos] = 0;
        this.hasFlashed.Add(pos);
        AddNeighbors(pos, this.revisit);
      }
    }

    /// <summary>
    /// Enumerates the all positions.
    /// </summary>
    /// <returns>A list of (int x, int y).</returns>
    private IEnumerable<(int x, int y)> EnumerateAllPositions()
    {
      for (int y = 0; y < this.data.Length; y++)
      {
        for (int x = 0; x < this.data[y].Length; x++)
        {
          yield return (x, y);
        }
      }
    }
  }
}