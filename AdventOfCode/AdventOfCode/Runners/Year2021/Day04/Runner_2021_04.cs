using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day04
{
  /// <summary>
  /// The runner_2021_04.
  /// </summary>
  public class Runner_2021_04
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_04"/> class.
    /// </summary>
    public Runner_2021_04()
      : base(2021, 4)
    {
    }

    /// <inheritdoc/>
    public override (string? part1, string? part2) Run(string inputString, string[] inputLines)
    {
      int[] draws = inputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

      var boards = new List<BingoBoard>();
      for (int i = 2; i < inputLines.Length; i += 6)
      {
        boards.Add(new BingoBoard(inputLines[i..(i + 5)], i));
      }

      int part1 = 0;
      int part2 = 0;
      var boardsToWatch = boards.Select(x => x.Identifier).ToList();
      foreach (int draw in draws)
      {
        var winnerBoards = boards.Where(x => x.MarkNumber(draw)).ToArray();
        if (winnerBoards.Length > 0)
        {
          foreach (var winningBoard in winnerBoards)
          {
            if (part1 == 0)
            {
              part1 = winningBoard.GetScore();
            }

            if (boardsToWatch.Count == 1 && winningBoard.Identifier == boardsToWatch[0])
            {
              part2 = winningBoard.GetScore();
            }

            boardsToWatch.Remove(winningBoard.Identifier);
          }
        }
      }

      return (part1.ToString(), part2.ToString());
    }
  }
}