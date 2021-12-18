using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day14
{
  /// <summary>
  /// Insertion lookup pair.
  /// </summary>
  public readonly struct InsertionLookupPair
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
    /// Initializes a new instance of the <see cref="InsertionLookupPair"/> class.
    /// </summary>
    /// <param name="chars">The chars.</param>
    public InsertionLookupPair(string chars)
      : this(chars[0], chars[1])
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InsertionLookupPair"/> class.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    public InsertionLookupPair(char left, char right) => (this.Left, this.Right) = (left, right);
  }
}