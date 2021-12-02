using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day02
{
  /// <summary>
  /// The sub.
  /// </summary>
  public class Sub
  {
    /// <summary>
    /// Whether to use aim.
    /// </summary>
    private readonly bool useAim;

    /// <summary>
    /// Aim.
    /// </summary>
    private int aim;

    /// <summary>
    /// Gets or sets the forward.
    /// </summary>
    public int Forward { get; set; }

    /// <summary>
    /// Gets or sets the depth.
    /// </summary>
    public int Depth { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sub"/> class.
    /// </summary>
    public Sub()
    {
      this.useAim = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sub"/> class.
    /// </summary>
    /// <param name="useAim">If true, use aim.</param>
    public Sub(bool useAim) => this.useAim = useAim;

    /// <summary>
    /// Performs the action.
    /// </summary>
    /// <param name="action">The action.</param>
    public void PerformAction(string action)
    {
      string[] parts = action.Split(' ');
      string direction = parts[0];
      int count = int.Parse(parts[1]);

      _ = direction switch
      {
        "forward" => this.MoveForward(count),
        "down" => this.MoveUpOrDown(count),
        "up" => this.MoveUpOrDown(-count),
        _ => throw new NotImplementedException()
      };
    }

    /// <summary>
    /// Moves the up or down.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An int.</returns>
    private int MoveUpOrDown(int count)
    {
      if (!this.useAim)
      {
        return this.Depth += count;
      }

      return this.aim += count;
    }

    /// <summary>
    /// Moves the forward.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An int.</returns>
    private int MoveForward(int count)
    {
      if (this.useAim)
      {
        this.Depth += this.aim * count;
      }

      return this.Forward += count;
    }
  }
}