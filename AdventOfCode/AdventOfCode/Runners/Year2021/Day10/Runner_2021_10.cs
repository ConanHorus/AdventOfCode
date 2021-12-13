using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day10
{
  /// <summary>
  /// The runner_2021_10.
  /// </summary>
  public class Runner_2021_10
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_10"/> class.
    /// </summary>
    public Runner_2021_10()
      : base(2021, 10)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var illigalCharacters = new List<char>();
      var incompleteLineScores = new List<ulong>();
      var stack = new Stack<char>();
      foreach (string line in inputLines)
      {
        bool wasIlligal = false;
        stack.Clear();
        foreach (char c in line)
        {
          if (c == '[' || c == '{' || c == '(' || c == '<')
          {
            stack.Push(c);
            continue;
          }

          char popped = stack.Pop();
          if (popped == '(')
          {
            if (c != ')')
            {
              wasIlligal = true;
              illigalCharacters.Add(c);
              break;
            }
          }
          else
          {
            if (c != popped + 2)
            {
              wasIlligal = true;
              illigalCharacters.Add(c);
              break;
            }
          }
        }

        if (!wasIlligal)
        {
          ulong incompleteScore = 0;
          while (stack.Count > 0)
          {
            char c = stack.Pop();
            incompleteScore *= 5;
            incompleteScore += c switch
            {
              '(' => 1,
              '[' => 2,
              '{' => 3,
              '<' => 4,
              _ => throw new NotImplementedException()
            };
          }

          incompleteLineScores.Add(incompleteScore);
        }
      }

      int part1 = SumIllegalCharacters(illigalCharacters);
      ulong part2 = incompleteLineScores.OrderBy(x => x).Skip(incompleteLineScores.Count / 2).First();

      return (part1, part2);
    }

    /// <summary>
    /// Sums the illegal characters.
    /// </summary>
    /// <param name="illigalCharacters">The illigal characters.</param>
    /// <returns>An int.</returns>
    private static int SumIllegalCharacters(List<char> illigalCharacters)
    {
      int sum = 0;
      foreach (char c in illigalCharacters)
      {
        sum += c switch
        {
          ')' => 3,
          ']' => 57,
          '}' => 1197,
          '>' => 25137,
          _ => throw new NotImplementedException()
        };
      }

      return sum;
    }
  }
}