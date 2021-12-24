using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day17
{
  /// <summary>
  /// The target.
  /// </summary>
  public class Target
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Target"/> class.
    /// </summary>
    /// <param name="line">The line.</param>
    public Target(string line)
    {
      string[] parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);
      string xString = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries)[0].Trim();
      string yString = parts[2].Trim();

      int[] xValues = xString.Split("..", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
      int[] yValues = yString.Split("..", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

      this.Left = Math.Min(xValues[0], xValues[1]);
      this.Right = Math.Max(xValues[0], xValues[1]);
      this.Bottom = Math.Min(yValues[0], yValues[1]);
      this.Top = Math.Max(yValues[0], yValues[1]);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Target"/> class.
    /// </summary>
    public Target()
    {
    }

    /// <summary>
    /// Gets or sets the left.
    /// </summary>
    public int Left { get; set; }

    /// <summary>
    /// Gets or sets the right.
    /// </summary>
    public int Right { get; set; }

    /// <summary>
    /// Gets or sets the top.
    /// </summary>
    public int Top { get; set; }

    /// <summary>
    /// Gets or sets the bottom.
    /// </summary>
    public int Bottom { get; set; }
  }
}