using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day12
{
  /// <summary>
  /// The runner_2021_12.
  /// </summary>
  public class Runner_2021_12
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_12"/> class.
    /// </summary>
    public Runner_2021_12()
      : base(2021, 12)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      int part1 = FindAllPaths(inputLines, false);
      int part2 = FindAllPaths(inputLines, true);

      return (part1, part2);
    }

    private static int FindAllPaths(string[] inputLines, bool allowReturnToSmallRooms)
    {
      var goodPaths = new List<Path>();
      var incompletePaths = new Queue<Path>();
      incompletePaths.Enqueue(new Path());

      var adjacencyMatrix = new AdjacencyMatrix(inputLines);

      while (incompletePaths.Count > 0)
      {
        var path = incompletePaths.Dequeue();
        foreach (var childPath in path.GenerateChildPaths(adjacencyMatrix, allowReturnToSmallRooms))
        {
          if (childPath.State == PathState.Bad)
          {
            continue;
          }

          if (childPath.State == PathState.KeepGoing)
          {
            incompletePaths.Enqueue(childPath);
            continue;
          }

          goodPaths.Add(childPath);
        }
      }

      int part1 = goodPaths.Count();
      return part1;
    }
  }
}