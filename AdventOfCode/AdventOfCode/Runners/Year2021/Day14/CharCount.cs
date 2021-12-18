using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day14
{
  /// <summary>
  /// Character count.
  /// </summary>
  public class CharCount
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CharCount"/> class.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <param name="count">The count.</param>
    public CharCount(char character, ulong count) => (this.Char, this.Count) = (character, count);

    /// <summary>
    /// Gets or sets character.
    /// </summary>
    public char Char { get; set; }

    /// <summary>
    /// Gets or sets count.
    /// </summary>
    public ulong Count { get; set; }
  }
}