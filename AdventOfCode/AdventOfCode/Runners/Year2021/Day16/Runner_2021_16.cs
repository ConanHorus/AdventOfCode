using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day16
{
  /// <summary>
  /// The runner_2021_16.
  /// </summary>
  public class Runner_2021_16
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_16"/> class.
    /// </summary>
    public Runner_2021_16()
      : base(2021, 16)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var bits = Message.ConvertHexidecimalToExpandedBits(inputString);
      var packetRoot = Message.ConvertBitsToPacketTree(bits);

      int part1 = packetRoot.EnumerateChildren().Select(x => (int)x.Version).Sum();
      packetRoot.Reduce();
      long part2 = packetRoot.Value;

      return (part1, part2);
    }
  }
}