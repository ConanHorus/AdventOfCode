using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day18
{
  /// <summary>
  /// The shuttle strategy.
  /// </summary>
  public interface IShuttleStrategy
  {
    /// <summary>
    /// Traverses the tree to destination.
    /// </summary>
    /// <param name="start">The start.</param>
    /// <returns>A SnailNumber?.</returns>
    SnailNumber? TraverseTreeToDestination(SnailNumber start);
  }
}