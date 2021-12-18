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
    private readonly string seed;

    /// <summary>
    /// The instructions.
    /// </summary>
    private readonly Dictionary<InsertionLookupPair, char> instructions;

    /// <summary>
    /// Memoized numbers.
    /// </summary>
    private readonly Dictionary<Memo, CharCount[]> memoized = new Dictionary<Memo, CharCount[]>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Polymer"/> class.
    /// </summary>
    /// <param name="seed">The seed.</param>
    /// <param name="instructions">Instructions</param>
    public Polymer(string seed, Dictionary<InsertionLookupPair, char> instructions) =>
      (this.seed, this.instructions) = (seed, instructions);

    /// <summary>
    /// Counts the chemical occurances after steps.
    /// </summary>
    /// <param name="steps">The steps.</param>
    /// <returns>A Dictionary.</returns>
    public Dictionary<char, ulong> CountChemicalOccurancesAfterSteps(int steps)
    {
      var list = new List<CharCount[]>();
      char last = this.seed[0];
      foreach (char c in this.seed.Skip(1))
      {
        list.Add(this.GetCharacterCounts(new InsertionLookupPair(last, c), steps));
        last = c;
      }

      var dictinary = new Dictionary<char, ulong>();
      foreach (var charCount in list.SelectMany(x => x))
      {
        if (!dictinary.ContainsKey(charCount.Char))
        {
          dictinary[charCount.Char] = 0;
        }

        dictinary[charCount.Char] += charCount.Count;
      }

      return dictinary;
    }

    /// <summary>
    /// Gets the character counts.
    /// </summary>
    /// <param name="pair">The pair.</param>
    /// <param name="remainingSteps">The remaining steps.</param>
    /// <returns>An array of CharCounts.</returns>
    private CharCount[] GetCharacterCounts(InsertionLookupPair pair, int remainingSteps)
    {
      var lookup = this.LoadMemo(pair, remainingSteps);
      if (lookup is not null)
      {
        return lookup;
      }

      if (remainingSteps == 0)
      {
        return Array.Empty<CharCount>();
      }

      CharCount[] left;
      CharCount[] right;
      if (remainingSteps == 1)
      {
        left = new CharCount[] { new CharCount(pair.Left, 1) };
        right = new CharCount[] { new CharCount(this.instructions[pair], 1) };
      }
      else
      {
        left = this.GetCharacterCounts(new InsertionLookupPair(pair.Left, this.instructions[pair]), remainingSteps - 1);
        right = this.GetCharacterCounts(new InsertionLookupPair(this.instructions[pair], pair.Right), remainingSteps - 1);
      }

      var combined = this.Combine(left, right);
      this.SaveMemo(pair, remainingSteps, combined);
      return combined;
    }

    /// <summary>
    /// Combines the left and right char count arrays.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns>An array of CharCounts.</returns>
    private CharCount[] Combine(CharCount[] left, CharCount[] right)
    {
      var dictionary = new Dictionary<char, ulong>();

      foreach (var charCount in left)
      {
        dictionary[charCount.Char] = charCount.Count;
      }

      foreach (var charCount in right)
      {
        if (!dictionary.ContainsKey(charCount.Char))
        {
          dictionary[charCount.Char] = 0;
        }

        dictionary[charCount.Char] += charCount.Count;
      }

      return dictionary.Select(x => new CharCount(x.Key, x.Value)).ToArray();
    }

    /// <summary>
    /// Saves the memo.
    /// </summary>
    /// <param name="pair">The pair.</param>
    /// <param name="remainingSteps">The remaining steps.</param>
    /// <param name="charCounts">The char counts.</param>
    private void SaveMemo(InsertionLookupPair pair, int remainingSteps, CharCount[] charCounts)
    {
      var memo = new Memo(pair, remainingSteps);
      if (this.memoized.ContainsKey(memo))
      {
        return;
      }

      this.memoized[memo] = charCounts;
    }

    /// <summary>
    /// Loads the memo.
    /// </summary>
    /// <param name="pair">The pair.</param>
    /// <param name="remainingSteps">The remaining steps.</param>
    /// <returns>A CharCount[]? .</returns>
    private CharCount[]? LoadMemo(InsertionLookupPair pair, int remainingSteps)
    {
      var memo = new Memo(pair, remainingSteps);
      if (!this.memoized.ContainsKey(memo))
      {
        return null;
      }

      return this.memoized[memo];
    }
  }
}