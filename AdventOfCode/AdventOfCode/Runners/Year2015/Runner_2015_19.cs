using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_2015_19.
  /// </summary>
  public class Runner_2015_19
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_19"/> class.
    /// </summary>
    public Runner_2015_19()
      : base(2015, 19)
    {
    }

    /// <inheritdoc/>
    public override (string? part1, string? part2) Run(string inputString, string[] inputLines)
    {
      (var conversions, string molecule) = ReadInput(inputLines);

      var part1 = new HashSet<string>();
      for (int i = 0; i < molecule.Length; i++)
      {
        FindAllOptionsAtPosition(part1, molecule, i, conversions);
      }

      int part2 = OperationsToElectron(molecule, conversions);

      return (part1.Count.ToString(), part2.ToString());
    }

    /// <summary>
    /// Operations the to electron.
    /// </summary>
    /// <param name="molecule">The molecule.</param>
    /// <param name="conversions">The conversions.</param>
    /// <returns>An int.</returns>
    private static int OperationsToElectron(string molecule, List<(string, string)> conversions)
    {
      var molecules = new Stack<string>();
      molecules.Push(molecule);

      int cycles = 0;
      while (!molecules.Contains("e"))
      {
        cycles++;
        molecules = FindAllPreviousMolecules(molecules, conversions);
        Console.WriteLine(molecules.OrderBy(x => x.Length).First());
      }

      return cycles;
    }

    /// <summary>
    /// Finds the all previous molecules.
    /// </summary>
    /// <param name="molecules">The molecules.</param>
    /// <param name="conversions">The conversions.</param>
    /// <returns>A Stack.</returns>
    private static Stack<string> FindAllPreviousMolecules(Stack<string> molecules, List<(string, string)> conversions)
    {
      var previousMolecules = new List<string>();

      while (molecules.Count > 0)
      {
        string molecule = molecules.Pop();
        foreach (string previousMolecule in FindAllPreviousMolecules(molecule, conversions))
        {
          previousMolecules.Add(previousMolecule);
        }
      }

      return new Stack<string>(previousMolecules.OrderBy(x => x.Length).Take(300));
    }

    /// <summary>
    /// Finds the all previous molecules.
    /// </summary>
    /// <param name="molecule">The molecule.</param>
    /// <param name="conversions">The conversions.</param>
    /// <returns>A list of string.</returns>
    private static IEnumerable<string> FindAllPreviousMolecules(string molecule, List<(string, string)> conversions)
    {
      for (int i = 0; i < molecule.Length; i++)
      {
        string substring = molecule.Substring(i);
        foreach ((string from, string to) in conversions)
        {
          if (substring.StartsWith(to))
          {
            yield return molecule.Substring(0, i) + from + molecule.Substring(i + to.Length);
          }
        }
      }
    }

    /// <summary>
    /// Reads the input.
    /// </summary>
    /// <param name="inputLines">The input lines.</param>
    /// <returns>A (List&lt;(string, string)&gt; conversions, string molecule) .</returns>
    private static (List<(string, string)> conversions, string molecule) ReadInput(string[] inputLines)
    {
      bool part1 = true;

      var conversions = new List<(string, string)>();
      string molecule = string.Empty;

      foreach (string line in inputLines)
      {
        if (string.IsNullOrWhiteSpace(line))
        {
          part1 = false;
          continue;
        }

        if (part1)
        {
          string[] parts = line.Split(" => ");
          conversions.Add((parts[0], parts[1]));
          continue;
        }

        molecule = line;
      }

      return (conversions, molecule);
    }

    /// <summary>
    /// Finds the all options at position.
    /// </summary>
    /// <param name="part1">The part1.</param>
    /// <param name="molecule">The molecule.</param>
    /// <param name="i">The i.</param>
    /// <param name="conversions">The conversions.</param>
    private static void FindAllOptionsAtPosition(
      HashSet<string> part1,
      string molecule,
      int i,
      List<(string, string)> conversions)
    {
      foreach ((string from, string to) in conversions)
      {
        if (i + from.Length > molecule.Length)
        {
          continue;
        }

        if (molecule.Substring(i, from.Length) != from)
        {
          continue;
        }

        string insert = molecule.Substring(0, i) + to + molecule.Substring(i + from.Length);
        part1.Add(insert);
      }
    }
  }
}