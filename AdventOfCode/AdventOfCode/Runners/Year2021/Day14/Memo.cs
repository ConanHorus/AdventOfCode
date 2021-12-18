using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day14
{
  /// <summary>
  /// Memo.
  /// </summary>
  public struct Memo
  {
    /// <summary>
    /// Left character.
    /// </summary>
    public readonly char Left;

    /// <summary>
    /// Right character.
    /// </summary>
    public readonly char Right;

    /// <summary>
    /// Steps.
    /// </summary>
    public readonly int Steps;

    /// <summary>
    /// Initializes a new instance of the <see cref="Memo"/> class.
    /// </summary>
    /// <param name="pair">The pair.</param>
    /// <param name="steps">The steps.</param>
    public Memo(InsertionLookupPair pair, int steps) => (this.Left, this.Right, this.Steps) = (pair.Left, pair.Right, steps);
  }
}