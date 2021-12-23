using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day15
{
  /// <summary>
  /// The position with heuristic.
  /// </summary>
  public class PositionWithHeuristic
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PositionWithHeuristic"/> class.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="distanceToEnd">The distance to end.</param>
    public PositionWithHeuristic(int x, int y, float distanceToEnd)
      : this(new Position(x, y), distanceToEnd)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PositionWithHeuristic"/> class.
    /// </summary>
    /// <param name="position">The position.</param>
    /// <param name="distanceToEnd">The distance to end.</param>
    public PositionWithHeuristic(Position position, float distanceToEnd) =>
      (this.Position, this.Risk, this.DistanceToEnd) = (position, 0, distanceToEnd);

    /// <summary>
    /// Gets or sets the position.
    /// </summary>
    public Position Position { get; set; }

    /// <summary>
    /// Gets or sets the risk.
    /// </summary>
    public ulong Risk { get; set; }

    /// <summary>
    /// Gets or sets the distance to end.
    /// </summary>
    public float DistanceToEnd { get; set; }

    /// <summary>
    /// Gets the hueristic.
    /// </summary>
    public float Hueristic => this.Risk + (DistanceToEnd * 2f);
  }
}