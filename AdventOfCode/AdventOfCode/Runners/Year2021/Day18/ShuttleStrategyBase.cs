using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day18
{
  /// <summary>
  /// The shuttle strategy base.
  /// </summary>
  public abstract class ShuttleStrategyBase
    : IShuttleStrategy
  {
    /// <summary>
    /// Whether to move left.
    /// </summary>
    private readonly bool moveLeft;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShuttleStrategyBase"/> class.
    /// </summary>
    /// <param name="moveLeft">If true, move left.</param>
    public ShuttleStrategyBase(bool moveLeft) => this.moveLeft = moveLeft;

    /// <inheritdoc/>
    public SnailNumber? TraverseTreeToDestination(SnailNumber start)
    {
      var current = MoveUp(start);
      var previous = start;
      if (current is null)
      {
        return null;
      }

      while (current is not null && !this.StartMovingDown(current, previous))
      {
        previous = current;
        current = MoveUp(current);
      }

      if (current is null)
      {
        return null;
      }

      return this.MoveDown(current);
    }

    /// <summary>
    /// Moves up the tree.
    /// </summary>
    /// <param name="from">The from.</param>
    /// <returns>A SnailNumber? .</returns>
    private static SnailNumber? MoveUp(SnailNumber from)
    {
      return from.Parent;
    }

    /// <summary>
    /// Whether to start moving down the three.
    /// </summary>
    /// <param name="snailNumber">The snail number.</param>
    /// <param name="previous">Previous snail number.</param>
    /// <returns>A bool.</returns>
    private bool StartMovingDown(SnailNumber snailNumber, SnailNumber previous)
    {
      if (this.moveLeft)
      {
        return snailNumber.RightSnailNumber == previous;
      }

      return snailNumber.LeftSnailNumber == previous;
    }

    /// <summary>
    /// Moves down the tree.
    /// </summary>
    /// <param name="from">The from.</param>
    /// <returns>A SnailNumber.</returns>
    private SnailNumber MoveDown(SnailNumber from)
    {
      bool first = true;
      var current = from;
      while (KeepMovingDown(current, first))
      {
        if (this.moveLeft)
        {
          if (first)
          {
            current = current.LeftSnailNumber!;
          }
          else
          {
            current = current.RightSnailNumber!;
          }
        }
        else
        {
          if (first)
          {
            current = current.RightSnailNumber!;
          }
          else
          {
            current = current.LeftSnailNumber!;
          }
        }

        first = false;
      }

      return current;
    }

    /// <summary>
    /// Whether to keep moving down.
    /// </summary>
    /// <param name="current">The current.</param>
    /// <param name="first">First way down.</param>
    /// <returns>A bool.</returns>
    private bool KeepMovingDown(SnailNumber current, bool first)
    {
      if (this.moveLeft)
      {
        if (first)
        {
          return current.LeftSnailNumber is not null;
        }
        else
        {
          return current.RightSnailNumber is not null;
        }
      }

      if (first)
      {
        return current.RightSnailNumber is not null;
      }
      else
      {
        return current.LeftSnailNumber is not null;
      }
    }
  }
}