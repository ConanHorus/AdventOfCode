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
    /// Gets the runner.
    /// </summary>
    /// <param name="year">The year.</param>
    /// <param name="day">The day.</param>
    /// <returns>A RunnerBase? .</returns>
    public static RunnerBase? GetRunner(int year, int day)
    {
      return runners.Value.Where(x => x is not null).FirstOrDefault(x => x!.Year == year && x.Day == day);
    }

    /// <summary>
    /// Runs correct runner.
    /// </summary>
    /// <param name="runner">Runner.</param>
    /// <returns>Result.</returns>
    public static (string? part1, string? part2) Run(RunnerBase runner)
    {
      return runner.Run(runner.GetInputString(), runner.GetInputLines());
    }
  }
}