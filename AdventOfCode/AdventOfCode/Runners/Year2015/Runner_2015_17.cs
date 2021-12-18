using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_17.
  /// </summary>
  public class Runner_2015_17
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_17"/> class.
    /// </summary>
    public Runner_2015_17()
      : base(2015, 17)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      int[] containers = inputLines.Select(x => int.Parse(x)).ToArray();
      var containerCombos = FindAllPermutations(containers)
        .Select(x => new { Count = x.Length, Sum = x.Sum() })
        .Where(x => x.Sum == 150)
        .ToArray();

      int part1 = containerCombos.Count();
      int minSize = containerCombos.Select(x => x.Count).Min();
      int part2 = containerCombos.Where(x => x.Count == minSize).Count();

      return (part1, part2);
    }

    /// <summary>
    /// Finds the all permutations.
    /// </summary>
    /// <param name="containers">The containers.</param>
    /// <returns>A list of int[].</returns>
    private static IEnumerable<int[]> FindAllPermutations(int[] containers)
    {
      uint maxValue = (uint)(1 << containers.Length);
      uint toUse = 0;

      var combo = new List<int>();

      do
      {
        combo.Clear();
        for (int i = 0; i < containers.Length; i++)
        {
          if ((toUse & 1 << i) != 0)
          {
            combo.Add(containers[i]);
          }
        }

        yield return combo.ToArray();

        toUse++;
      }
      while (toUse < maxValue);
    }
  }
}