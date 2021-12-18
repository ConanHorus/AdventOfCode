using System.Linq;

namespace AdventOfCode.Runners.Year2021.Day09
{
  /// <summary>
  /// The runner_2021_09.
  /// </summary>
  public class Runner_2021_09
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_09"/> class.
    /// </summary>
    public Runner_2021_09()
      : base(2021, 9)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var grid = new Grid(inputLines);
      int part1 = grid.GetAllLocalMinima().Select(x => x + 1).Sum();
      int part2 = grid.GetBasinSizes().OrderByDescending(x => x).Take(3).Aggregate((x, y) => x * y);
      return (part1, part2);
    }
  }
}