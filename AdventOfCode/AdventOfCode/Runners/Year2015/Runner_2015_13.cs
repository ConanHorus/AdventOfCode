using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_13.
  /// </summary>
  public class Runner_2015_13
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_13"/> class.
    /// </summary>
    public Runner_2015_13()
      : base(2015, 13)
    {
    }

    /// <summary>
    /// Runs the.
    /// </summary>
    /// <param name="inputString">The input string.</param>
    /// <param name="inputLines">The input lines.</param>
    /// <returns>A (string? part1, string? part2) .</returns>
    public override (string? part1, string? part2) Run(string inputString, string[] inputLines)
    {
      var happinessMatrix = GenerateHappinessMatrix(inputLines);
      var names = new List<string>();
      foreach (string name in happinessMatrix.Keys.Select(x => x.from))
      {
        if (!names.Contains(name))
        {
          names.Add(name);
        }
      }

      int part1 = FindAllTableSeatings(names).Select(x => x.CalculateHappiness(happinessMatrix)).Max();

      foreach (string name in names)
      {
        happinessMatrix[("me", name)] = 0;
        happinessMatrix[(name, "me")] = 0;
      }

      names.Add("me");

      int part2 = FindAllTableSeatings(names).Select(x => x.CalculateHappiness(happinessMatrix)).Max();

      return (part1.ToString(), part2.ToString());
    }

    /// <summary>
    /// Generates the happiness matrix.
    /// </summary>
    /// <param name="inputLines">The input lines.</param>
    /// <returns>A Dictionary.</returns>
    private static Dictionary<(string from, string to), int> GenerateHappinessMatrix(string[] inputLines)
    {
      var matrix = new Dictionary<(string from, string to), int>();

      foreach (string line in inputLines)
      {
        string[] lineParts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        string name1 = lineParts[0];
        string name2 = lineParts[^1].Trim('.');
        int value = int.Parse(lineParts[3]);
        if (lineParts[2] == "lose")
        {
          value *= -1;
        }

        matrix[(name1, name2)] = value;
      }

      return matrix;
    }

    /// <summary>
    /// Finds the all table seatings.
    /// </summary>
    /// <param name="names">The names.</param>
    /// <returns>A list of TableSpots.</returns>
    private static IEnumerable<TableSpot> FindAllTableSeatings(List<string> names)
    {
      var places = new List<TableSpot>();

      foreach (string[] permutation in FindAllPermutations(names))
      {
        places.Clear();
        foreach (string name in permutation)
        {
          places.Add(new TableSpot { Seated = name });
        }

        for (int i = 0; i < places.Count; i++)
        {
          if (i == 0)
          {
            places[i].Right = places[i + 1];
            places[i].Left = places[^1];
            continue;
          }

          if (i == places.Count - 1)
          {
            places[i].Right = places[0];
            places[i].Left = places[i - 1];
            continue;
          }

          places[i].Right = places[i + 1];
          places[i].Left = places[i - 1];
        }

        yield return places[0];
      }
    }

    /// <summary>
    /// Finds the all permutations.
    /// </summary>
    /// <param name="names">The names.</param>
    /// <returns>A list of string[].</returns>
    private static IEnumerable<string[]> FindAllPermutations(List<string> names)
    {
      int[] order = new int[names.Count];

      string[] buffer = new string[order.Length];

      while (IncrementOrder(ref order))
      {
        for (int i = 0; i < order.Length; i++)
        {
          buffer[i] = names[order[i]];
        }

        yield return buffer;
      }
    }

    /// <summary>
    /// Increments the order.
    /// </summary>
    /// <param name="order">The order.</param>
    /// <returns>A bool.</returns>
    private static bool IncrementOrder(ref int[] order)
    {
      bool first = true;
      while (first || TwoOrMoreAreEqual(order))
      {
        first = false;
        int ptr = order.Length - 1;
NEXT:
        order[ptr]++;
        if (order[ptr] >= order.Length)
        {
          order[ptr] = 0;
          ptr--;
          if (ptr < 0)
          {
            return false;
          }

          goto NEXT;
        }
      }

      return true;
    }

    /// <summary>
    /// Twos the or more are equal.
    /// </summary>
    /// <param name="order">The order.</param>
    /// <returns>A bool.</returns>
    private static bool TwoOrMoreAreEqual(int[] order)
    {
      var numbers = new List<int>();
      foreach (int number in order)
      {
        if (numbers.Contains(number))
        {
          return true;
        }

        numbers.Add(number);
      }

      return false;
    }

    /// <summary>
    /// The table spot.
    /// </summary>
    private class TableSpot
    {
      /// <summary>
      /// Whether this table spot has been visited.
      /// </summary>
      private bool visited;

      /// <summary>
      /// Gets or sets the seated.
      /// </summary>
      public string Seated { get; set; } = null!;

      /// <summary>
      /// Gets or sets the left.
      /// </summary>
      public TableSpot Left { get; set; } = null!;

      /// <summary>
      /// Gets or sets the right.
      /// </summary>
      public TableSpot Right { get; set; } = null!;

      /// <summary>
      /// Calculates the happiness.
      /// </summary>
      /// <param name="happinessMatrix">Happiness matrix.</param>
      /// <returns>An int.</returns>
      public int CalculateHappiness(Dictionary<(string from, string to), int> happinessMatrix)
      {
        if (this.visited)
        {
          return 0;
        }

        this.visited = true;
        return
          happinessMatrix[(this.Seated, this.Left.Seated)] +
          happinessMatrix[(this.Seated, this.Right.Seated)] +
          this.Right.CalculateHappiness(happinessMatrix);
      }
    }
  }
}