using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day18
{
  /// <summary>
  /// The left shuttle strategy.
  /// </summary>
  public sealed class LeftShuttleStrategy
    : ShuttleStrategyBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="LeftShuttleStrategy"/> class.
    /// </summary>
    public LeftShuttleStrategy()
      : base(moveLeft: true)
    {
    }
  }
}