using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Runners.Year2021.Day03
{
  /// <summary>
  /// The runner_2021_03.
  /// </summary>
  public class Runner_2021_03
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_03"/> class.
    /// </summary>
    public Runner_2021_03()
      : base(2021, 3)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var rawBits = inputLines.Select(x => x.ToArray().Select(y => (y - '0')).ToArray()).ToList();

      int gamma = FindGama(rawBits);
      int epsilon = FindEpsilon(rawBits);

      int oxygen = FindOxygen(rawBits);
      int co2 = FindCo2(rawBits);

      int part1 = gamma * epsilon;
      int part2 = oxygen * co2;

      return (part1, part2);
    }

    /// <summary>
    /// Finds the co2.
    /// </summary>
    /// <param name="rawBits">The raw bits.</param>
    /// <returns>An int.</returns>
    private static int FindCo2(List<int[]> rawBits)
    {
      var culled = new List<int[]>(rawBits);
      int index = 0;
      while (culled.Count > 1)
      {
        int bitValue = FindLeastCommonBit(culled, index);
        culled = culled.Where(x => x[index] == bitValue).ToList();
        index++;
      }

      int value = 0;
      int[] culledValue = culled[0];
      for (int i = 0; i < culledValue.Length; i++)
      {
        value <<= 1;
        value += culledValue[i];
      }

      return value;
    }

    /// <summary>
    /// Finds the oxygen.
    /// </summary>
    /// <param name="rawBits">The raw bits.</param>
    /// <returns>An int.</returns>
    private static int FindOxygen(List<int[]> rawBits)
    {
      var culled = new List<int[]>(rawBits);
      int index = 0;
      while (culled.Count > 1)
      {
        int bitValue = FindMostCommonBit(culled, index);
        culled = culled.Where(x => x[index] == bitValue).ToList();
        index++;
      }

      int value = 0;
      int[] culledValue = culled[0];
      for (int i = 0; i < culledValue.Length; i++)
      {
        value <<= 1;
        value += culledValue[i];
      }

      return value;
    }

    /// <summary>
    /// Finds the most common bit.
    /// </summary>
    /// <param name="rawBits">The raw bits.</param>
    /// <param name="position">The position.</param>
    /// <returns>An int.</returns>
    private static int FindMostCommonBit(List<int[]> rawBits, int position)
    {
      int onBits = rawBits.Select(x => x[position]).Sum();
      int offBits = rawBits.Count - onBits;
      return onBits >= offBits ? 1 : 0;
    }

    /// <summary>
    /// Finds the least common bit.
    /// </summary>
    /// <param name="rawBits">The raw bits.</param>
    /// <param name="position">The position.</param>
    /// <returns>An int.</returns>
    private static int FindLeastCommonBit(List<int[]> rawBits, int position)
    {
      return FindMostCommonBit(rawBits, position) == 1 ? 0 : 1;
    }

    /// <summary>
    /// Finds the gama.
    /// </summary>
    /// <param name="rawBits">The raw bits.</param>
    /// <returns>An int.</returns>
    private static int FindGama(List<int[]> rawBits)
    {
      int value = 0;
      for (int i = 0; i < rawBits[0].Length; i++)
      {
        value <<= 1;
        value += FindMostCommonBit(rawBits, i);
      }

      return value;
    }

    /// <summary>
    /// Finds the epsilon.
    /// </summary>
    /// <param name="rawBits">The raw bits.</param>
    /// <returns>An int.</returns>
    private static int FindEpsilon(List<int[]> rawBits)
    {
      int value = 0;
      for (int i = 0; i < rawBits[0].Length; i++)
      {
        value <<= 1;
        value += FindLeastCommonBit(rawBits, i);
      }

      return value;
    }
  }
}