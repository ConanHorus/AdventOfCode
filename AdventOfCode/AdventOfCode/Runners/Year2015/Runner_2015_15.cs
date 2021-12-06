using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Runners.Year2015
{
  /// <summary>
  /// The runner_15_15.
  /// </summary>
  public class Runner_2015_15
    : RunnerBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Runner_2015_15"/> class.
    /// </summary>
    public Runner_2015_15()
      : base(2015, 15)
    {
    }

    /// <inheritdoc/>
    public override (object? part1, object? part2) Run(string inputString, string[] inputLines)
    {
      var ingredeints = inputLines.Select(x => new Ingredient(x)).ToList();
      int part1 = FindAllPortions(ingredeints.Count).Select(x => CalculateScore(x, ingredeints)).Max();
      int part2 = FindAllPortions(ingredeints.Count)
        .Where(x => CalculateCalories(x, ingredeints) == 500)
        .Select(x => CalculateScore(x, ingredeints))
        .Max();

      return (part1, part2);
    }

    /// <summary>
    /// Finds the all portions.
    /// </summary>
    /// <param name="numberOfIngredients">The number of ingredients.</param>
    /// <returns>A list of int[].</returns>
    private static IEnumerable<int[]> FindAllPortions(int numberOfIngredients)
    {
      int[] portions = new int[numberOfIngredients];
      bool keepFinding = true;
      while (keepFinding)
      {
        if (portions.Sum() == 100)
        {
          yield return portions;
        }

        bool reset;
        int ptr = numberOfIngredients - 1;
        do
        {
          reset = false;
          portions[ptr]++;
          if (portions[ptr] > 100)
          {
            portions[ptr] = 0;
            ptr--;
            reset = true;
          }
        }
        while (reset && ptr >= 0);

        if (ptr < 0)
        {
          keepFinding = false;
        }
      }
    }

    /// <summary>
    /// Calculates the calories.
    /// </summary>
    /// <param name="portions">The portions.</param>
    /// <param name="ingredients">The ingredients.</param>
    /// <returns>An int.</returns>
    private static int CalculateCalories(int[] portions, List<Ingredient> ingredients)
    {
      int calories = 0;
      for (int i = 0; i < portions.Length; i++)
      {
        var ingredient = ingredients[i];
        int multiplier = portions[i];

        calories += ingredient.Calories * multiplier;
      }

      return calories;
    }

    /// <summary>
    /// Calculates the score.
    /// </summary>
    /// <param name="portions">The portions.</param>
    /// <param name="ingredients">The ingredients.</param>
    /// <returns>An int.</returns>
    private static int CalculateScore(int[] portions, List<Ingredient> ingredients)
    {
      int[] scorePortions = new int[4];
      for (int i = 0; i < portions.Length; i++)
      {
        var ingredient = ingredients[i];
        int multiplier = portions[i];

        scorePortions[0] += ingredient.Capacity * multiplier;
        scorePortions[1] += ingredient.Durability * multiplier;
        scorePortions[2] += ingredient.Flavor * multiplier;
        scorePortions[3] += ingredient.Texture * multiplier;
      }

      for (int i = 0; i < scorePortions.Length; i++)
      {
        scorePortions[i] = Math.Max(0, scorePortions[i]);
      }

      return scorePortions[0] * scorePortions[1] * scorePortions[2] * scorePortions[3];
    }

    /// <summary>
    /// The ingredient.
    /// </summary>
    private class Ingredient
    {
      /// <summary>
      /// Gets or sets the name.
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// Gets or sets the capacity.
      /// </summary>
      public int Capacity { get; set; }

      /// <summary>
      /// Gets or sets the durability.
      /// </summary>
      public int Durability { get; set; }

      /// <summary>
      /// Gets or sets the flavor.
      /// </summary>
      public int Flavor { get; set; }

      /// <summary>
      /// Gets or sets the texture.
      /// </summary>
      public int Texture { get; set; }

      /// <summary>
      /// Gets or sets the calories.
      /// </summary>
      public int Calories { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Ingredient"/> class.
      /// </summary>
      /// <param name="description">The description.</param>
      public Ingredient(string description)
      {
        this.Name = description.Split(':')[0];
        var parts = description
          .Split(':')[1]
          .Split(',')
          .Select(x =>
          {
            string[] subParts = x.Trim().Split(' ');
            return new { Property = subParts[0], Value = int.Parse(subParts[1]) };
          })
          .ToArray();

        foreach (var part in parts)
        {
          _ = part.Property switch
          {
            "capacity" => this.Capacity = part.Value,
            "durability" => this.Durability = part.Value,
            "flavor" => this.Flavor = part.Value,
            "texture" => this.Texture = part.Value,
            "calories" => this.Calories = part.Value,
            _ => throw new NotImplementedException()
          };
        }
      }
    }
  }
}