using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day14
{
  /// <summary>
  /// The runner_2021_14.
  /// </summary>
  public class Runner_2021_14
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2021_14"/> class.
    /// </summary>
    public Runner_2021_14()
      : base(2021, 14)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var instructions = GenerateInstructions(inputLines[2..]);
      var polymer = new Polymer(inputLines[0], instructions);

      var counts1 = polymer.CountChemicalOccurancesAfterSteps(10);
      var counts2 = polymer.CountChemicalOccurancesAfterSteps(40);

      ulong part1 = counts1.Select(x => x.Value).Max() + 1 - counts1.Select(x => x.Value).Min();
      ulong part2 = counts2.Select(x => x.Value).Max() + 1 - counts2.Select(x => x.Value).Min();

      return (part1, part2);
    }

    /// <summary>
    /// Generates the instructions.
    /// </summary>
    /// <param name="lines">The lines.</param>
    /// <returns>A Dictionary.</returns>
    private Dictionary<InsertionLookupPair, char> GenerateInstructions(string[] lines)
    {
      var instructions = new Dictionary<InsertionLookupPair, char>();

      foreach (string line in lines)
      {
        string[] parts = line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
        instructions[new InsertionLookupPair(parts[0])] = parts[1][0];
      }

      return instructions;
    }
  }
}