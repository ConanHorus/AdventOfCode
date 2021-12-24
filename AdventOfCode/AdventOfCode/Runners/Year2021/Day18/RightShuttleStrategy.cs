using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day18
{
  /// <summary>
  /// The right shuttle strategy.
  /// </summary>
  public sealed class RightShuttleStrategy
    : ShuttleStrategyBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RightShuttleStrategy"/> class.
    /// </summary>
    public RightShuttleStrategy()
      : base(moveLeft: false)
    {
    }
  }
}