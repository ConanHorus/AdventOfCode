using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day12
{
  /// <summary>
  /// The path state.
  /// </summary>
  public enum PathState
  {
    /// <summary>
    /// Incomplete.
    /// </summary>
    KeepGoing,

    /// <summary>
    /// Complete.
    /// </summary>
    Good,

    /// <summary>
    /// Invalid.
    /// </summary>
    Bad
  }
}