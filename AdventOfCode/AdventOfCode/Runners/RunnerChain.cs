using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners
{
  /// <summary>
  /// The runner chain.
  /// </summary>
  public static class RunnerChain
  {
    /// <summary>
    /// List of runners.
    /// </summary>
    private static readonly Lazy<List<RunnerBase?>> runners = new Lazy<List<RunnerBase?>>(() =>
    {
      Type[] types = Assembly.GetExecutingAssembly().GetTypes();
      return types.Where(x => x is not null && x != typeof(RunnerBase))
        .Where(x => x.IsSubclassOf(typeof(RunnerBase)))
        .Select(x => Activator.CreateInstance(x) as RunnerBase)
        .ToList();
    });

    /// <summary>
    /// Runs correct runner.
    /// </summary>
    /// <param name="year">Year.</param>
    /// <param name="day">Day.</param>
    /// <returns>Result.</returns>
    public static (string? part1, string? part2)? Run(int year, int day)
    {
      var runner = runners.Value.Where(x => x is not null)
        .FirstOrDefault(x => x!.Year == year && x.Day == day);

      if (runner is null)
      {
        return null;
      }

      return runner.Run(runner.GetInputString(), runner.GetInputLines());
    }
  }
}