using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day15
{
  /// <summary>
  /// The ordered positions with heuristics.
  /// </summary>
  public class OrderedPositionsWithHeuristics
  {
    /// <summary>
    /// Positions.
    /// </summary>
    private readonly LinkedList<PositionWithHeuristic> positions = new LinkedList<PositionWithHeuristic>();

    /// <summary>
    /// Adds the position with heuristic.
    /// </summary>
    /// <param name="position">The position.</param>
    public void AddPositionWithHeuristic(PositionWithHeuristic position)
    {
      var extantPosition = this.positions
        .Where(x => x.Position.X == position.Position.X && x.Position.Y == position.Position.Y)
        .FirstOrDefault();
      if (extantPosition is null)
      {
        this.InsertPosition(position);
        return;
      }

      this.UpdatePosition(extantPosition, position);
    }

    /// <summary>
    /// Pulls the first.
    /// </summary>
    /// <returns>A PositionWithHeuristic.</returns>
    public PositionWithHeuristic PullFirst()
    {
      var first = this.positions.First!.Value;
      this.positions.RemoveFirst();
      return first;
    }

    /// <summary>
    /// Inserts the position.
    /// </summary>
    /// <param name="position">The position.</param>
    private void InsertPosition(PositionWithHeuristic position)
    {
      var node = this.positions.First;
      if (node is null)
      {
        this.positions.AddFirst(position);
        return;
      }

      while (node is not null)
      {
        if (node.Value.Hueristic > position.Hueristic)
        {
          this.positions.AddBefore(node, position);
          return;
        }

        node = node.Next;
      }

      this.positions.AddLast(position);
    }

    /// <summary>
    /// Updates the position.
    /// </summary>
    /// <param name="extantPosition">The extant position.</param>
    /// <param name="position">The position.</param>
    private void UpdatePosition(PositionWithHeuristic extantPosition, PositionWithHeuristic position)
    {
      if (extantPosition.Hueristic <= position.Hueristic)
      {
        return;
      }

      this.RemovePosition(extantPosition);
      this.InsertPosition(position);
    }

    /// <summary>
    /// Removes the position.
    /// </summary>
    /// <param name="position">The position.</param>
    private void RemovePosition(PositionWithHeuristic position)
    {
      this.positions.Remove(position);
    }
  }
}