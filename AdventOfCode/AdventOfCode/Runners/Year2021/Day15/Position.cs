using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day15
{
  /// <summary>
  /// The position.
  /// </summary>
  public struct Position
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Position"/> class.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    public Position(int x, int y) => (this.X, this.Y) = (x, y);

    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    public int Y { get; set; }
  }
}