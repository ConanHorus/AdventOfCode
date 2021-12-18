using System.Linq;

namespace AdventOfCode.Runners.Year2021.Day01
{
  /// <summary>
  /// The runner_2021_01.
  /// </summary>
  public class Runner_2021_01
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_01"/> class.
    /// </summary>
    public Runner_2021_01()
      : base(2021, 1)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      int[] depths = inputLines.Select(x => int.Parse(x)).ToArray();

      int part1 = FindNumberOfIncreases(depths);
      int part2 = FindIncreasesWithSlidingWindows(depths);

      return (part1, part2);
    }

    /// <summary>
    /// Finds the number of increases.
    /// </summary>
    /// <param name="depths">The input depths.</param>
    /// <returns>An int.</returns>
    private static int FindNumberOfIncreases(int[] depths)
    {
      int lastValue = int.MaxValue;
      int increaseCount = 0;
      foreach (int depth in depths)
      {
        if (depth > lastValue)
        {
          increaseCount++;
        }

        lastValue = depth;
      }

      int part1 = increaseCount;
      return part1;
    }

    /// <summary>
    /// Finds the increases with sliding windows.
    /// </summary>
    /// <param name="depths">The input depths.</param>
    /// <returns>An int.</returns>
    private static int FindIncreasesWithSlidingWindows(int[] depths)
    {
      int[] windows = new int[depths.Length - 2];
      for (int i = 0; i < depths.Length - 2; i++)
      {
        windows[i] = depths[i..(i + 3)].Sum();
      }

      return FindNumberOfIncreases(windows);
    }
  }
}