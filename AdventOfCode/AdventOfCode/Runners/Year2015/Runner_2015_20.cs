using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_2015_20.
  /// </summary>
  [TimeWarning]
  public class Runner_2015_20
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_20"/> class.
    /// </summary>
    public Runner_2015_20()
      : base(2015, 20)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      int target = int.Parse(inputString);

      int number = 665000;
      bool foundTarget = false;
      do
      {
        number++;

        int calculated = FindAllFactors(number).Sum() * 10;
        if (calculated >= target)
        {
          foundTarget = true;
        }
      }
      while (!foundTarget);

      int part1 = number;

      number = 0;
      foundTarget = false;
      var happyFactors = new Dictionary<int, int>();
      do
      {
        number++;

        int calculated = FindAllNonExhaustedFactors(number, happyFactors).Sum() * 11;

        if (calculated >= target)
        {
          foundTarget = true;
        }
      }
      while (!foundTarget);

      int part2 = number;

      return (part1, part2);
    }

    /// <summary>
    /// Finds the all factors.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>A list of int.</returns>
    private static IEnumerable<int> FindAllFactors(int number)
    {
      if (number <= 0)
      {
        yield break;
      }

      int endPoint = (number / 2) + 1;
      for (int i = 1; i <= endPoint; i++)
      {
        if (number % i == 0)
        {
          yield return i;
        }
      }

      yield return number;
    }

    /// <summary>
    /// Finds the all non exhausted factors.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="happyFactors">The happy factors.</param>
    /// <returns>A list of int.</returns>
    private static IEnumerable<int> FindAllNonExhaustedFactors(int number, Dictionary<int, int> happyFactors)
    {
      if (number <= 0)
      {
        yield break;
      }

      int endPoint = (number / 2) + 1;
      foreach (int factor in happyFactors.Keys)
      {
        if (factor > endPoint)
        {
          continue;
        }

        if (number % factor == 0)
        {
          yield return factor;
          happyFactors[factor]++;
          if (happyFactors[factor] > 50)
          {
            happyFactors.Remove(factor);
          }
        }
      }

      yield return number;
      happyFactors[number] = 1;
    }
  }
}