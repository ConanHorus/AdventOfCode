using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day04
{
  /// <summary>
  /// The bingo board.
  /// </summary>
  public class BingoBoard
  {
    /// <summary>
    /// Current state.
    /// </summary>
    private readonly List<List<BingoSquare>> state = new List<List<BingoSquare>>();

    /// <summary>
    /// Last number.
    /// </summary>
    private int lastNumber;

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public int Identifier { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BingoBoard"/> class.
    /// </summary>
    /// <param name="boardInfo">The board info.</param>
    /// <param name="identifier">Identifier.</param>
    public BingoBoard(string[] boardInfo, int identifier)
    {
      this.Identifier = identifier;
      foreach (string line in boardInfo)
      {
        this.state.Add(
          line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => new BingoSquare(int.Parse(x))).ToList());
      }
    }

    /// <summary>
    /// Marks the number.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>A bool.</returns>
    public bool MarkNumber(int number)
    {
      foreach (var square in this.state.SelectMany(x => x))
      {
        if (square.Number == number)
        {
          square.Marked = true;
          this.lastNumber = number;
          break;
        }
      }

      return this.HasWinState();
    }

    /// <summary>
    /// Gets the score.
    /// </summary>
    /// <returns>An int.</returns>
    public int GetScore()
    {
      return this.state.SelectMany(x => x).Where(x => !x.Marked).Select(x => x.Number).Sum() * this.lastNumber;
    }

    /// <summary>
    /// Have the win state.
    /// </summary>
    /// <returns>A bool.</returns>
    private bool HasWinState()
    {
      if (this.CheckRows())
      {
        return true;
      }

      if (this.CheckColumns())
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// Checks the columns.
    /// </summary>
    /// <returns>A bool.</returns>
    private bool CheckColumns()
    {
      foreach (var subList in this.state)
      {
        if (subList.Select(x => x.Marked).All(x => x))
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Checks the rows.
    /// </summary>
    /// <returns>A bool.</returns>
    private bool CheckRows()
    {
      for (int iRow = 0; iRow < this.state[0].Count; iRow++)
      {
        bool allMarked = true;
        foreach (var subList in this.state)
        {
          if (!subList[iRow].Marked)
          {
            allMarked = false;
          }
        }

        if (allMarked)
        {
          return true;
        }
      }

      return false;
    }
  }
}