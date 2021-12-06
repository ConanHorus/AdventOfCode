using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_16.
  /// </summary>
  public class Runner_2015_16
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_16"/> class.
    /// </summary>
    public Runner_2015_16()
      : base(2015, 16)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var target = new Dictionary<string, int>
      {
        { "children", 3 },
        { "cats", 7 },
        { "samoyeds", 2 },
        { "pomeranians", 3 },
        { "akitas", 0 },
        { "vizslas", 0 },
        { "goldfish", 5 },
        { "trees", 3 },
        { "cars", 2 },
        { "perfumes", 1}
      };

      int part1 = 0;
      int part2 = 0;

      var aunts = inputLines.Select(x => new Aunt(x)).ToArray();
      foreach (var aunt in aunts)
      {
        bool found1 = true;
        bool found2 = true;
        foreach (var pair in target)
        {
          if (!aunt.MatchesValue(pair))
          {
            found1 = false;
          }

          if (!aunt.MatchesValueRange(pair))
          {
            found2 = false;
          }
        }

        if (found1)
        {
          part1 = aunt.Number;
        }

        if (found2)
        {
          part2 = aunt.Number;
        }
      }

      return (part1, part2);
    }

    /// <summary>
    /// The aunt.
    /// </summary>
    private class Aunt
    {
      /// <summary>
      /// Details.
      /// </summary>
      private Dictionary<string, int> details = new Dictionary<string, int>();

      /// <summary>
      /// Gets or sets the number.
      /// </summary>
      public int Number { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Aunt"/> class.
      /// </summary>
      /// <param name="description">The description.</param>
      public Aunt(string description)
      {
        this.Number = int.Parse(description.Split(' ')[1].Trim(':'));
        foreach (string detailString in string.Concat(description.Split(' ')[2..^0]).Split(','))
        {
          string[] detailParts = detailString.Split(':');
          this.details[detailParts[0]] = int.Parse(detailParts[1]);
        }
      }

      /// <summary>
      /// Matches the value.
      /// </summary>
      /// <param name="keyValuePair">The key value pair.</param>
      /// <returns>A bool.</returns>
      public bool MatchesValue(KeyValuePair<string, int> keyValuePair)
      {
        if (!this.details.ContainsKey(keyValuePair.Key))
        {
          return true;
        }

        return this.details[keyValuePair.Key] == keyValuePair.Value;
      }

      /// <summary>
      /// Matches the value range.
      /// </summary>
      /// <param name="keyValuePair">The key value pair.</param>
      /// <returns>A bool.</returns>
      public bool MatchesValueRange(KeyValuePair<string, int> keyValuePair)
      {
        if (!this.details.ContainsKey(keyValuePair.Key))
        {
          return true;
        }

        string key = keyValuePair.Key;
        int value = keyValuePair.Value;

        if (key == "cats" || key == "trees")
        {
          return this.details[key] > value;
        }

        if (key == "pomeranians" || key == "goldfish")
        {
          return this.details[key] < value;
        }

        return this.details[key] == value;
      }
    }
  }
}