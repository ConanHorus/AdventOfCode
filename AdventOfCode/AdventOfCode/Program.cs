using AdventOfCode.Runners;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
  /// <summary>
  /// The program.
  /// </summary>
  internal class Program
  {
    /// <summary>
    /// Saved colors.
    /// </summary>
    private static readonly Stack<ConsoleColor> savedColors = new Stack<ConsoleColor>();

    /// <summary>
    /// Program entry point.
    /// </summary>
    /// <param name="args">Args.</param>
    private static void Main(string[] args)
    {
      do
      {
        int year = GetYear();
        int day = GetDay();
        var results = RunnerChain.Run(year, day);
        DisplayResults(results);
      }
      while (GetContinue());
    }

    /// <summary>
    /// Displays results.
    /// </summary>
    /// <param name="results"></param>
    private static void DisplayResults((string? part1, string? part2)? results)
    {
      SaveConsoleColor();
      Console.WriteLine();
      if (results is null)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Runner for year and day was not found.");
      }

      if (results is not null)
      {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Part 1: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(results?.part1 is null ? "null" : results?.part1);

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Part 2: ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(results?.part2 is null ? "null" : results?.part2);
      }

      RestoreConsoleColor();
    }

    /// <summary>
    /// Gets year from user.
    /// </summary>
    /// <returns></returns>
    private static int GetYear()
    {
      Console.WriteLine();
      Console.WriteLine("Please enter a year");
      return GetInteger();
    }

    /// <summary>
    /// Gets day from user.
    /// </summary>
    /// <returns></returns>
    private static int GetDay()
    {
      Console.WriteLine();
      Console.WriteLine("Please enter a day");
      return GetInteger();
    }

    /// <summary>
    /// Gets integer from user.
    /// </summary>
    /// <returns>Integer from user.</returns>
    private static int GetInteger()
    {
      int number;
      bool firstRun = true;
      string? input;
      do
      {
        if (!firstRun)
        {
          SaveConsoleColor();
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine();
          Console.WriteLine("That is not a valid integer, please try again.");
          RestoreConsoleColor();
        }

        firstRun = false;
        SaveConsoleColor();
        DrawInputCarrot();
        input = Console.ReadLine();
        RestoreConsoleColor();
      }
      while (!int.TryParse(input, out number));

      return number;
    }

    /// <summary>
    /// Gets whether to continue.
    /// </summary>
    /// <returns>Whther to continue.</returns>
    private static bool GetContinue()
    {
      Console.WriteLine();
      Console.WriteLine("Do you wish to continue? ( y / enter ) -> continue / any key -> end");
      var input = ReadInputKey();

      return input.Key == ConsoleKey.Enter || input.KeyChar == 'y' || input.KeyChar == 'Y';
    }

    /// <summary>
    /// Reads input key.
    /// </summary>
    /// <returns>Console key info.</returns>
    private static ConsoleKeyInfo ReadInputKey()
    {
      SaveConsoleColor();
      DrawInputCarrot();
      var input = Console.ReadKey();
      RestoreConsoleColor();
      return input;
    }

    private static void DrawInputCarrot()
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.Write("> ");
    }

    /// <summary>
    /// Saves console color.
    /// </summary>
    private static void SaveConsoleColor()
    {
      savedColors.Push(Console.ForegroundColor);
    }

    /// <summary>
    /// Restores console color.
    /// </summary>
    private static void RestoreConsoleColor()
    {
      Console.ForegroundColor = savedColors.Pop();
    }
  }
}