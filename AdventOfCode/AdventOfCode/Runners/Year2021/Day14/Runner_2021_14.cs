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
      var polymer = new Polymer(inputLines[0]);
      var instructions = GenerateInstructions(inputLines[2..]);
      var chemicalCounts1 = new Dictionary<char, ulong>();
      var chemicalCounts2 = new Dictionary<char, ulong>();

      foreach (char c in polymer.CombineForSteps(instructions, 10))
      {
        AddToChemicalCounts(chemicalCounts1, c);
      }

      foreach (char c in polymer.CombineForSteps(instructions, 40))
      {
        AddToChemicalCounts(chemicalCounts2, c);
      }

      ulong part1 = chemicalCounts1.Select(x => x.Value).Max() - chemicalCounts1.Select(x => x.Value).Min();
      ulong part2 = chemicalCounts2.Select(x => x.Value).Max() - chemicalCounts2.Select(x => x.Value).Min();

      return (part1, part2);
    }

    /// <summary>
    /// Adds the chemical to chemical counts.
    /// </summary>
    /// <param name="chemicalCounts">The chemical counts.</param>
    /// <param name="c">The c.</param>
    private void AddToChemicalCounts(Dictionary<char, ulong> chemicalCounts, char c)
    {
      if (!chemicalCounts.ContainsKey(c))
      {
        chemicalCounts.Add(c, 0);
      }

      chemicalCounts[c]++;
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