using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2021.Day14
{
  /// <summary>
  /// The polymer.
  /// </summary>
  public class Polymer
  {
    /// <summary>
    /// The seed.
    /// </summary>
    public readonly string seed;

    /// <summary>
    /// Initializes a new instance of the <see cref="Polymer"/> class.
    /// </summary>
    /// <param name="seed">The seed.</param>
    public Polymer(string seed) => this.seed = seed;

    /// <summary>
    /// Combines the for steps.
    /// </summary>
    /// <param name="instructions">The instructions.</param>
    /// <param name="steps">The steps.</param>
    /// <returns>A list of char.</returns>
    public IEnumerable<char> CombineForSteps(Dictionary<InsertionLookupPair, char> instructions, int steps)
    {
      var enumaration = this.seed.Select(x => x);
      for (int i = 0; i < steps; i++)
      {
        enumaration = SingleStep(enumaration, instructions);
      }

      return enumaration;
    }

    /// <summary>
    /// Performs a step.
    /// </summary>
    /// <param name="pair">The pair.</param>
    /// <param name="instructions">The instructions.</param>
    /// <param name="stepsRemaining">The steps remaining.</param>
    /// <returns>A list of char.</returns>
    private static IEnumerable<char> RecursiveStep(
      InsertionLookupPair pair,
      Dictionary<InsertionLookupPair, char> instructions,
      int stepsRemaining)
    {
      char center = instructions[pair];

      if (stepsRemaining == 0)
      {
        yield return center;
        yield break;
      }

      foreach (char c in RecursiveStep(new InsertionLookupPair(pair.Left, center), instructions, stepsRemaining - 1))
      {
        yield return c;
      }

      yield return center;
      foreach (char c in RecursiveStep(new InsertionLookupPair(center, pair.Right), instructions, stepsRemaining - 1))
      {
        yield return c;
      }
    }

    /// <summary>
    /// Single step.
    /// </summary>
    /// <param name="seed">The seed.</param>
    /// <param name="instructions">The instructions.</param>
    /// <returns>A list of char.</returns>
    private static IEnumerable<char> SingleStep(IEnumerable<char> seed, Dictionary<InsertionLookupPair, char> instructions)
    {
      char last = seed.First();
      yield return last;
      foreach (char c in seed.Skip(1))
      {
        yield return instructions[new InsertionLookupPair(last, c)];
        yield return c;
        last = c;
      }
    }
  }
}