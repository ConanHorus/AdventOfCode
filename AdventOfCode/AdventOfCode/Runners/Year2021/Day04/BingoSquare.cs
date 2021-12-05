using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day04
{
  /// <summary>
  /// The bingo square.
  /// </summary>
  public class BingoSquare
  {
    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether marked.
    /// </summary>
    public bool Marked { get; set; } = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="BingoSquare"/> class.
    /// </summary>
    /// <param name="number">The number.</param>
    public BingoSquare(int number) => this.Number = number;
  }
}