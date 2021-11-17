using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_3.
  /// </summary>
  public class Runner_2015_03
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_03"/> class.
    /// </summary>
    public Runner_2015_03()
      : base(2015, 3)
    {
    }

    /// <inheritdoc/>
    public override (string? part1, string? part2) Run(string inputString, string[] inputLines)
    {
      var world = new HashSet<(int, int)>();
      var santa = new Visitor();
      world.Add((0, 0));
      foreach (char c in inputString)
      {
        santa.Move(c);
        if (!world.Contains(santa.GetLocation()))
        {
          world.Add(santa.GetLocation());
        }
      }

      int part1 = world.Count;

      world = new HashSet<(int, int)>();
      santa = new Visitor();
      var robot = new Visitor();
      bool turn = true;
      world.Add((0, 0));
      foreach (char c in inputString)
      {
        if (turn)
        {
          santa.Move(c);
        }
        else
        {
          robot.Move(c);
        }

        turn = !turn;

        if (!world.Contains(santa.GetLocation()))
        {
          world.Add(santa.GetLocation());
        }

        if (!world.Contains(robot.GetLocation()))
        {
          world.Add(robot.GetLocation());
        }
      }

      int part2 = world.Count;

      return (part1.ToString(), part2.ToString());
    }

    /// <summary>
    /// The visitor.
    /// </summary>
    private class Visitor
    {
      /// <summary>
      /// X.
      /// </summary>
      public int x = 0;

      /// <summary>
      /// Y.
      /// </summary>
      private int y = 0;

      /// <summary>
      /// Gets location.
      /// </summary>
      /// <returns>Location.</returns>
      public (int x, int y) GetLocation() => (this.x, this.y);

      /// <summary>
      /// Causes visitor to move.
      /// </summary>
      /// <param name="c">Instruction.</param>
      public void Move(char c)
      {
        _ = c switch
        {
          '^' => this.y++,
          'v' => this.y--,
          '>' => this.x++,
          '<' => this.x--,
        };
      }
    }
  }
}