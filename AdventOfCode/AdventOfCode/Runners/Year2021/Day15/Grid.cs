using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day15
{
  /// <summary>
  /// The grid.
  /// </summary>
  public class Grid
  {
    /// <summary>
    /// Grid data.
    /// </summary>
    private readonly byte[,] gridData;

    /// <summary>
    /// Width of grid data.
    /// </summary>
    private readonly int width;

    /// <summary>
    /// Height of grid data.
    /// </summary>
    private readonly int height;

    /// <summary>
    /// Input width.
    /// </summary>
    private readonly int inputWidth;

    /// <summary>
    /// Input height.
    /// </summary>
    private readonly int inputHeight;

    /// <summary>
    /// Initializes a new instance of the <see cref="Grid"/> class.
    /// </summary>
    /// <param name="lines">The lines.</param>
    /// <param name="fiveTimes">Whether five tiems as large.</param>
    public Grid(string[] lines, bool fiveTimes)
    {
      this.inputWidth = lines[0].Length;
      this.inputHeight = lines.Length;
      this.width = fiveTimes ? this.inputWidth * 5 : this.inputWidth;
      this.height = fiveTimes ? this.inputHeight * 5 : this.inputHeight;

      this.gridData = new byte[this.width, this.height];
      for (int y = 0; y < lines.Length; y++)
      {
        for (int x = 0; x < lines[y].Length; x++)
        {
          byte value = (byte)(lines[y][x] - '0');
          this.gridData[x, y] = value;
        }
      }

      if (fiveTimes)
      {
        this.ExtendGridData();
      }
    }

    /// <summary>
    /// Calculates the best path risk.
    /// </summary>
    /// <returns>An ulong.</returns>
    public ulong CalculateBestPathRisk()
    {
      var positions = new OrderedPositionsWithHeuristics();
      positions.AddPositionWithHeuristic(new PositionWithHeuristic(0, 0, CalculateDistanceToEnd(new Position(0, 0))));
      var visited = new HashSet<Position>();

      bool reachedEnd = false;
      PositionWithHeuristic? endPosition = null;
      while (!reachedEnd)
      {
        var position = positions.PullFirst();
        visited.Add(position.Position);
        foreach (var next in this.GetNextMoveOptions(position))
        {
          if (this.IsAtEnd(next.Position))
          {
            reachedEnd = true;
            endPosition = next;
            break;
          }

          if (!visited.Contains(next.Position))
          {
            positions.AddPositionWithHeuristic(next);
          }
        }
      }

      return endPosition!.Risk;
    }

    /// <summary>
    /// Extends the grid data.
    /// </summary>
    private void ExtendGridData()
    {
      this.ExtendGridHorizontally(0);
      this.ExtendGridHorizontally(1);
      this.ExtendGridHorizontally(2);
      this.ExtendGridHorizontally(3);

      this.ExtendGridVertically(0);
      this.ExtendGridVertically(1);
      this.ExtendGridVertically(2);
      this.ExtendGridVertically(3);
    }

    /// <summary>
    /// Extends the grid horizontally.
    /// </summary>
    /// <param name="offset">The offset.</param>
    private void ExtendGridHorizontally(int offset)
    {
      for (int x = 0; x < this.inputWidth; x++)
      {
        for (int y = 0; y < this.inputHeight; y++)
        {
          int xx = x + (offset * this.inputWidth);
          int yy = y;
          byte value = (byte)(this.gridData[xx, yy] + 1);
          if (value == 10)
          {
            value = 1;
          }

          this.gridData[xx + this.inputWidth, yy] = value;
        }
      }
    }

    /// <summary>
    /// Extends the grid vertically.
    /// </summary>
    /// <param name="offset">The offset.</param>
    private void ExtendGridVertically(int offset)
    {
      for (int x = 0; x < this.width; x++)
      {
        for (int y = 0; y < this.inputHeight; y++)
        {
          int xx = x;
          int yy = y + (offset * this.inputHeight);
          byte value = (byte)(this.gridData[xx, yy] + 1);
          if (value == 10)
          {
            value = 1;
          }

          this.gridData[xx, yy + this.inputHeight] = value;
        }
      }
    }

    /// <summary>
    /// Gets the next move options.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns>A list of PositionWithHeuristics.</returns>
    private IEnumerable<PositionWithHeuristic> GetNextMoveOptions(PositionWithHeuristic position)
    {
      var north = new Position(position.Position.X, position.Position.Y - 1);
      var east = new Position(position.Position.X + 1, position.Position.Y);
      var south = new Position(position.Position.X, position.Position.Y + 1);
      var west = new Position(position.Position.X - 1, position.Position.Y);

      if (this.IsPositionInBounds(south))
      {
        yield return new PositionWithHeuristic(south, CalculateDistanceToEnd(south))
        {
          Risk = position.Risk + this.gridData[south.X, south.Y]
        };
      }

      if (this.IsPositionInBounds(east))
      {
        yield return new PositionWithHeuristic(east, CalculateDistanceToEnd(east))
        {
          Risk = position.Risk + this.gridData[east.X, east.Y]
        };
      }

      if (this.IsPositionInBounds(north))
      {
        yield return new PositionWithHeuristic(north, CalculateDistanceToEnd(north))
        {
          Risk = position.Risk + this.gridData[north.X, north.Y]
        };
      }

      if (this.IsPositionInBounds(west))
      {
        yield return new PositionWithHeuristic(west, CalculateDistanceToEnd(west))
        {
          Risk = position.Risk + this.gridData[west.X, west.Y]
        };
      }
    }

    /// <summary>
    /// Calculates the distance to end.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns>A float.</returns>
    private float CalculateDistanceToEnd(Position position)
    {
      return (float)Math.Sqrt(Math.Pow(position.X - this.width - 1, 2) + Math.Pow(position.Y - this.height - 1, 2));
    }

    /// <summary>
    /// Whether the position is in bounds.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns>A bool.</returns>
    private bool IsPositionInBounds(Position position)
    {
      return position.X >= 0 && position.X < this.width && position.Y >= 0 && position.Y < this.height;
    }

    /// <summary>
    /// Whether position is at the end.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns>A bool.</returns>
    private bool IsAtEnd(Position position)
    {
      return position.X == this.width - 1 && position.Y == this.height - 1;
    }
  }
}