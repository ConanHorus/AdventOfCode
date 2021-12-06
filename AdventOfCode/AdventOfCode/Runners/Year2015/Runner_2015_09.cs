using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_9.
  /// </summary>
  public class Runner_2015_09
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_09"/> class.
    /// </summary>
    public Runner_2015_09()
      : base(2015, 9)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var cities = new List<string>();
      var cityMap = new Dictionary<(string, string), int>();

      foreach (var line in inputLines)
      {
        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var cityCombo = (parts[0], parts[2]);
        cityMap[cityCombo] = int.Parse(parts[4]);
        cityCombo = (parts[2], parts[0]);
        cityMap[cityCombo] = int.Parse(parts[4]);
        if (!cities.Contains(parts[0]))
        {
          cities.Add(parts[0]);
        }

        if (!cities.Contains(parts[2]))
        {
          cities.Add(parts[2]);
        }
      }

      int shortest = int.MaxValue;
      int longest = 0;
      foreach (var combo in FindAllPermutations(cities))
      {
        int length = 0;
        for (int i = 1; i < combo.Length; i++)
        {
          length += cityMap[(combo[i - 1], combo[i])];
        }

        if (length < shortest)
        {
          shortest = length;
        }

        if (length > longest)
        {
          longest = length;
        }
      }

      int part1 = shortest;
      int part2 = longest;

      return (part1, part2);
    }

    /// <summary>
    /// Finds the all permutations.
    /// </summary>
    /// <param name="cities">The cities.</param>
    /// <returns>A list of string[].</returns>
    private static IEnumerable<string[]> FindAllPermutations(List<string> cities)
    {
      int[] order = new int[cities.Count];

      string[] buffer = new string[order.Length];

      while (IncrementOrder(ref order))
      {
        for (int i = 0; i < order.Length; i++)
        {
          buffer[i] = cities[order[i]];
        }

        yield return buffer;
      }
    }

    /// <summary>
    /// Increments the order.
    /// </summary>
    /// <param name="order">The order.</param>
    /// <returns>A bool.</returns>
    private static bool IncrementOrder(ref int[] order)
    {
      bool first = true;
      while (first || TwoOrMoreAreEqual(order))
      {
        first = false;
        int ptr = order.Length - 1;
NEXT:
        order[ptr]++;
        if (order[ptr] >= order.Length)
        {
          order[ptr] = 0;
          ptr--;
          if (ptr < 0)
          {
            return false;
          }

          goto NEXT;
        }
      }

      return true;
    }

    /// <summary>
    /// Twos the or more are equal.
    /// </summary>
    /// <param name="order">The order.</param>
    /// <returns>A bool.</returns>
    private static bool TwoOrMoreAreEqual(int[] order)
    {
      var numbers = new List<int>();
      foreach (int number in order)
      {
        if (numbers.Contains(number))
        {
          return true;
        }

        numbers.Add(number);
      }

      return false;
    }
  }
}