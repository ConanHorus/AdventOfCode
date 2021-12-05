using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day05
{
  /// <summary>
  /// Vector 2.
  /// </summary>
  public struct Vector2
  {
    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2"/> class.
    /// </summary>
    /// <param name="point">The point.</param>
    public Vector2(string point)
    {
      string[] parts = point.Split(',');
      this.X = int.Parse(parts[0]);
      this.Y = int.Parse(parts[1]);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Vector2"/> class.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    public Vector2(int x, int y) => (this.X, this.Y) = (x, y);

    /// <summary>
    /// Are the horizontal or vertical.
    /// </summary>
    /// <param name="a">The a.</param>
    /// <param name="b">The b.</param>
    /// <returns>A bool.</returns>
    public static bool AreHorizontalOrVertical(Vector2 a, Vector2 b)
    {
      return a.X == b.X || a.Y == b.Y;
    }
  }
}