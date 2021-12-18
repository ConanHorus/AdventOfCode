using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Runners.Year2021.Day09
{
  /// <summary>
  /// The grid.
  /// </summary>
  public class Grid
  {
    /// <summary>
    /// Data.
    /// </summary>
    private readonly string[] data;

    /// <summary>
    /// Initializes a new instance of the <see cref="Grid"/> class.
    /// </summary>
    /// <param name="lines">The lines.</param>
    public Grid(string[] lines) => this.data = lines;

    /// <summary>
    /// Gets all local minima.
    /// </summary>
    /// <returns>A list of byte.</returns>
    public IEnumerable<byte> GetAllLocalMinima()
    {
      return this.GetAllLocalMinimaLocations().Select(x => (byte)(this.GetLocation(x.x, x.y) - '0'));
    }

    /// <summary>
    /// Gets all local minima locations.
    /// </summary>
    /// <returns>A list of (int x, int y).</returns>
    public IEnumerable<(int x, int y)> GetAllLocalMinimaLocations()
    {
      for (int x = 0; x < this.data.Length; x++)
      {
        for (int y = 0; y < this.data[x].Length; y++)
        {
          if (this.IsLocalMinimum(x, y))
          {
            yield return (x, y);
          }
        }
      }
    }

    /// <summary>
    /// Gets the basin sizes.
    /// </summary>
    /// <returns>A list of int.</returns>
    public IEnumerable<int> GetBasinSizes()
    {
      foreach (var position in this.GetAllLocalMinimaLocations())
      {
        yield return this.FindBasinSize(position);
      }
    }

    /// <summary>
    /// Finds the basin size.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <returns>An int.</returns>
    private int FindBasinSize((int x, int y) position)
    {
      var toVisit = new Queue<(int x, int y)>();
      var visited = new HashSet<(int x, int y)> { position };
      var inBasin = new HashSet<(int x, int y)> { position };

      toVisit.Enqueue((position.x, position.y - 1));
      toVisit.Enqueue((position.x + 1, position.y));
      toVisit.Enqueue((position.x, position.y + 1));
      toVisit.Enqueue((position.x - 1, position.y));

      var slopesTo = new List<(int x, int y)>(4);
      while (toVisit.Count > 0)
      {
        var pos = toVisit.Dequeue();
        visited.Add(pos);
        char l = this.GetLocation(pos.x, pos.y);
        if (l == '9')
        {
          continue;
        }

        (var nPos, var ePos, var sPos, var wPos) = this.GetNeighborPositions(pos.x, pos.y);
        bool added = false;
        slopesTo.Clear();
        if (this.GetLocation(nPos.x, nPos.y) < l)
        {
          if (inBasin.Contains(nPos))
          {
            added = true;
            inBasin.Add(pos);
            VisitLater(nPos, ePos, sPos, wPos);
          }

          slopesTo.Add(nPos);
        }

        if (this.GetLocation(ePos.x, ePos.y) < l)
        {
          if (inBasin.Contains(ePos))
          {
            added = true;
            inBasin.Add(pos);
            VisitLater(nPos, ePos, sPos, wPos);
          }

          slopesTo.Add(ePos);
        }

        if (this.GetLocation(sPos.x, sPos.y) < l)
        {
          if (inBasin.Contains(sPos))
          {
            added = true;
            inBasin.Add(pos);
            VisitLater(nPos, ePos, sPos, wPos);
          }

          slopesTo.Add(sPos);
        }

        if (this.GetLocation(wPos.x, wPos.y) < l)
        {
          if (inBasin.Contains(wPos))
          {
            added = true;
            inBasin.Add(pos);
            VisitLater(nPos, ePos, sPos, wPos);
          }

          slopesTo.Add(wPos);
        }

        if (!added && slopesTo.Where(x => toVisit.Contains(x)).Count() > 0)
        {
          toVisit.Enqueue(pos);
        }
      }

      return inBasin.Count();

      void VisitLater((int x, int y) nPos, (int x, int y) ePos, (int x, int y) sPos, (int x, int y) wPos)
      {
        VisitLaterHelper(nPos);
        VisitLaterHelper(ePos);
        VisitLaterHelper(sPos);
        VisitLaterHelper(wPos);
      }

      void VisitLaterHelper((int x, int y) pos)
      {
        if (visited!.Contains(pos))
        {
          return;
        }

        if (inBasin!.Contains(pos))
        {
          return;
        }

        toVisit!.Enqueue(pos);
      }
    }

    /// <summary>
    /// Gets the neighbor positions.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>A ((int x, int y) nPos, (int x, int y) ePos, (int x, int y) sPos, (int x, int y) wPos).</returns>
    private ((int x, int y) nPos, (int x, int y) ePos, (int x, int y) sPos, (int x, int y) wPos)
      GetNeighborPositions(int x, int y)
    {
      return ((x, y - 1), (x + 1, y), (x, y + 1), (x - 1, y));
    }

    /// <summary>
    /// Gets the neighbor values.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>A (char n, char e, char s, char w) .</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private (char n, char e, char s, char w) GetNeighborValues(int x, int y)
    {
      return (
        this.GetLocation(x, y - 1),
        this.GetLocation(x + 1, y),
        this.GetLocation(x, y + 1),
        this.GetLocation(x - 1, y));
    }

    /// <summary>
    /// Are the local minimum.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>A bool.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private bool IsLocalMinimum(int x, int y)
    {
      char l = this.GetLocation(x, y);

      (char n, char e, char s, char w) = this.GetNeighborValues(x, y);
      return l < n && l < e && l < s && l < w;
    }

    /// <summary>
    /// Gets the location.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>A char.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private char GetLocation(int x, int y)
    {
      if (x < 0 || x >= this.data.Length || y < 0 || y >= this.data[0].Length)
      {
        return '9';
      }

      return this.data[x][y];
    }
  }
}