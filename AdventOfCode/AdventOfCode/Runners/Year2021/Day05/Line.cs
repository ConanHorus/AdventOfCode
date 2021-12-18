using System;
using System.Collections.Generic;

namespace AdventOfCode.Runners.Year2021.Day05
{
  /// <summary>
  /// The line.
  /// </summary>
  public class Line
  {
    /// <summary>
    /// Gets or sets the start.
    /// </summary>
    public Vector2 Start { get; set; }

    /// <summary>
    /// Gets or sets the end.
    /// </summary>
    public Vector2 End { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Line"/> class.
    /// </summary>
    /// <param name="line">The line.</param>
    public Line(string line)
    {
      string[] parts = line.Split(" -> ");
      this.Start = new Vector2(parts[0]);
      this.End = new Vector2(parts[1]);
    }

    /// <summary>
    /// Iterrates the all points.
    /// </summary>
    /// <returns>A list of Vector2S.</returns>
    public IEnumerable<Vector2> IterrateAllPoints()
    {
      if (this.Start.X == this.End.X)
      {
        for (int y = Math.Min(this.Start.Y, this.End.Y); y <= Math.Max(this.Start.Y, this.End.Y); y++)
        {
          yield return new Vector2(this.Start.X, y);
        }

        yield break;
      }

      if (this.Start.Y == this.End.Y)
      {
        for (int x = Math.Min(this.Start.X, this.End.X); x <= Math.Max(this.Start.X, this.End.X); x++)
        {
          yield return new Vector2(x, this.Start.Y);
        }

        yield break;
      }

      int x0 = this.Start.X;
      int dx = this.Start.X < this.End.X ? 1 : -1;
      int xEnd = this.End.X;
      int y0 = this.Start.Y;
      int dy = this.Start.Y < this.End.Y ? 1 : -1;
      int yEnd = this.End.Y;

      yield return new Vector2(x0, y0);
      while (x0 != xEnd && y0 != yEnd)
      {
        x0 += dx;
        y0 += dy;
        yield return new Vector2(x0, y0);
      }
    }

    /// <summary>
    /// Are the horizontal or vertical.
    /// </summary>
    /// <returns>A bool.</returns>
    public bool IsHorizontalOrVertical() => Vector2.AreHorizontalOrVertical(this.Start, this.End);
  }
}